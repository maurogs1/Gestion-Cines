//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BaseClass
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Text.RegularExpressions;

    public partial class Customer : IDataErrorInfo
    {

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Customer()
        {
            this.Ticket = new HashSet<Ticket>();

        }

        public int Id { get; set; }
        public string Dni { get; set; }
        public string Name { get; set; }
        public string Lastname { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Ticket> Ticket { get; set; }
        public string this[string columnName]
        {
            get
            {
                string msgError = null;
                switch (columnName)
                {
                    case "Dni":
                        msgError = validateDniCustomer();
                        break;

                    case "Lastname":
                        msgError = validateLastName();
                        break;
                    case "Name":
                        msgError = validateName();
                        break;
                    case "Phone":
                        msgError = validatePhoneCustomer();
                        break;
                    case "Email":
                        msgError = validateEmail();
                        break;
                }
                return msgError;
            }

        }

        public string Error => throw new NotImplementedException();

        private string validateDniCustomer()
        {

            if (String.IsNullOrEmpty(Dni))
            {
                return "Dni es requerido";
            }
            
            else if (Dni.Length < 8 && validate(Dni, @"[0-9]") == true)
            {
                return "Dni debe tener 8 digitos";
            }
            
            
            return null;
        }

        private string validatePhoneCustomer()
        {
            
            if (String.IsNullOrEmpty(Phone))
            {
                return "Telefono es requerido";
            }
            else if (Phone.Length < 10 && validate(Phone, @"[0-9]") == true)
            {
                return "Phone debe tener 10 digitos";
            }

            else if (validate(Phone, @"^[a-z A-Z]+$") == true)
            {
                return "Valor no valido";
            }
            else if (validate(Phone, @"[-.,;:_+}{´*¿¡'?=)(/&%#!°|@$]") == true)
            {
                return "Valor no valido";
            }
            return null;
        }
        //nuevo

        private string validateLastName()
        {
            if (String.IsNullOrEmpty(Lastname))
            {
                return "Apellido es requerido";
            }

            else if (validate(Lastname, @"[0-9]") == true)
            {
                return "Valor no valido";
            }

            else if (validate(Lastname, @"[-.,;:_+}{´*¿¡'?=)(/&%#!°|@$]") == true)
            {
                return "Valor no valido";
            }
            //else if (validate(Lastname, @"^[a-z A-Z]+$") == false)
            //{

            //}
            return null;
        }

        private string validateName()
        {
            if (String.IsNullOrEmpty(Name))
            {
                return "Nombre es requerido";
            }
            else if (validate(Name, @"[0-9]") == true)
            {
                return "Valor no valido";
            }
            else if (validate(Name, @"[-.,;:_+}{´*¿¡'?=)(/&%#!°|@$]") == true)
            {
                return "Valor no valido";
            }
            //else if (validate(Name, @"^[a-z A-Z]+$") == false)
            //{

            //}
            return null;
        }

        private string validateEmail()
        {
            if (String.IsNullOrEmpty(Email))
            {
                return "Email es requerido";
            }

            else if (validate(Email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$") == false)
            {
                return "Email no invalido";
            }
            return null;
        }

        private bool validate(string value, string pattern)
        {
            Regex regex = new Regex(pattern);
            if (regex.IsMatch(value))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool IsValidated()
        {
            if (this.validateDniCustomer() == null && this.validatePhoneCustomer() == null
                && this.validateEmail() == null && this.validateLastName() == null && this.validateName() == null)
                return true;
            return false;
        }
    }
}
