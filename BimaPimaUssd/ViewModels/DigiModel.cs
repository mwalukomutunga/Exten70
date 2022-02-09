using BimaPimaUssd.Contracts;
using BimaPimaUssd.Helpers;
using BimaPimaUssd.Models;
using BimaPimaUssd.Repository;
using System;
using System.Collections.Generic;

namespace BimaPimaUssd.ViewModels
{
    public class FTMAModel
    {

        private readonly ServerResponse serverResponse;
        private readonly IRepository repository;
        private readonly IMessager messager;
        private readonly Stack<int> levels;
        private readonly Repository<PBI> _repository;

        public string _Name { get; }
        public FTMAModel(ServerResponse serverResponse, IRepository repository, IMessager messager, IStoreDatabaseSettings settings)
        {
            _Name = serverResponse.PhoneNumber;
            this.serverResponse = serverResponse;
            this.repository = repository;
            this.messager = messager;
            levels = repository.levels[serverResponse.SessionId];
            _repository = new Repository<PBI>(settings, "PBIs");
        }
        public string MainMenu => levels.Pop() switch
        {
            0 => PlantingMonth,
            1 => CheckExisting,
            2 => ChooseFarmerType,
            3 => ProcessCode,
            4 => GetMonth,
            5 => GetWeek,
            6 => EnterAmount,
            7 => EnterPhone,
            8 => ProcessMpesaPayment,
            9 => Pay,
            10 => ConfirmPay,
            11 => ConfirmPayOptions,
            12 => ProcessConfirmation,
            13 => ProcessMpesaConfirmation,
            14 => ProcessValueChains,
            15 => EnterCustomPhone,
            _ => IFVM.Invalid
        };
       
      
        private string PlantingMonth
        {
            get
            {
                levels.Push(1);
                return IFVM.CheckExisting();
            }
        }
        private string CheckExisting
        {
            get
            {
                levels.Push(2);
                return IFVM.CheckExisting();
            }
        }
        private string ChooseFarmerType
        {
            get
            {                
                return repository.Requests[serverResponse.SessionId].Last.Value switch
                {
                    "1" => ProcessExisting(),
                    "2" => ProcessNew(),
                    _ =>IFVM.Invalid
                };
            }
        }

        public string ProcessCode 
        {
            get
            {
                levels.Push(4);
                var value = ValidateFarmerCode(repository.Requests[serverResponse.SessionId].Last.Value.Trim().ToString());
               return  value is null ? IFVM.LoadValueChains() : value.ToString();
            }
        }
        public string ProcessValueChains
        {
            get
            {
                levels.Push(4);
                return IFVM.LoadValueChains();
            }
        }

        public string GetMonth {
            get
            {
                levels.Push(5);
                return IFVM.TypeMonth();
            }
        }

       
        public string GetWeek
        {
            get
            {
                levels.Push(6);
                return IFVM.GetWeek();
            }
        }

        public string Pay
        {
            get
            {
                levels.Push(10);
                return IFVM.SelectPayMethod();
            }
        }
        public string ConfirmPay
        {
            get
            {
              
                return repository.Requests[serverResponse.SessionId].Last.Value switch
                {
                    "1" => CashConfirmPay(),
                    "2" => MpesaConfirmPay(),
                    _ => IFVM.Invalid
                };
            }
        }
        public string CashConfirmPay()
        {
            levels.Push(12);
            return IFVM.ConfirmPay(1000, 200);
        }
        public string MpesaConfirmPay()
        {
            levels.Push(13);
            return IFVM.ConfirmPay(1000, 200);
        }
        public string ProcessMpesaConfirmation
        {
            get
            {
               
                return repository.Requests[serverResponse.SessionId].Last.Value switch
                {
                    "1" => EnterPhone,
                    "2" => IFVM.ProcessCancel(),
                    _ => IFVM.Invalid
                };
            }
        }


        public string ProcessConfirmation
        {
            get
            {
                levels.Push(9);
                return repository.Requests[serverResponse.SessionId].Last.Value switch
                {
                    "1" => IFVM.ProcessCash(),
                    "2" => IFVM.ProcessCancel(),
                    _ => IFVM.Invalid
                };
            }
        }

        public string ConfirmPayOptions
        {
            get
            {
                levels.Push(9);
                return repository.Requests[serverResponse.SessionId].Last.Value switch
                {
                    "1" => IFVM.ProcessCash(),
                    "2" => IFVM.ProcessMpesa(),
                    _ => IFVM.Invalid
                };
            }
        }
        public string EnterAmount
        {
            get
            {
                levels.Push(9);
                return IFVM.GetAmount();
            }
        }
        public string EnterPhone
        {
            get
            {
                levels.Push(15);
                return IFVM.GetPhone(serverResponse.PhoneNumber);
            }
        }

        public string EnterCustomPhone
        {
            get
            {
                levels.Push(8);
                return repository.Requests[serverResponse.SessionId].Last.Value switch
                {
                    "1" => ProcessMpesaPayment,
                    "2" => IFVM.EnterMpesaNo(),
                    _ => IFVM.Invalid
                };
            }
        }
        public string ProcessMpesaPayment
        {
            get
            {///
                //add prompt to mpesa
                return IFVM.ProcessMpesa();

                //    repository.Requests[serverResponse.SessionId].Last.Value switch
                //{
                //    "1" => Pay,
                //    "2" => Pay,
                //    _ => IFVM.Invalid
                //};
            }
        }


        private string ValidateFarmerCode(string code)
        {
            if (_repository.GetByProperty("farmercode", code) is null)
            {
                levels.Pop();
                levels.Push(3);                
                return IFVM.InvalidCode;
            }
            return null;
        }

        private string ProcessNew()
        {
            return ProcessValueChains;
        }

        private string ProcessExisting()
        {
            levels.Push(3);
            return IFVM.CollectCode();
        }

       

    }
}
