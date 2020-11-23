using BaseClass.IDataAccess;
using BaseClass.Model;
using BaseClass.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseClass.DataAccess
{


    public class WorkCustomer : IWorkGeneric<Customer>
    {

        public WorkCustomer()
        {

        }


        public override void Add(Customer entity) // añadir un nuevo cliente
        {
            em.Customer.Add(entity);
            em.SaveChanges();
        }

        public bool AddCustom(Customer entity) // añadir un nuevo cliente
        {
            if (entity.IsValidated())
            {
                em.Customer.Add(entity);
                em.SaveChanges();
                return true;
            }

            return false;
        }

        public override void Update(Customer entity) //actualizar un cliente
        {
            Customer customer = em.Customer.Where(m => m.Id == entity.Id).First<Customer>();
            if (customer != null)
            {
                em.Entry(customer).CurrentValues.SetValues(entity);
                //customer = entity;
                em.SaveChanges();
            }
            else
                throw new ArgumentException("Cliente no encontrado");

        }
        public bool UpdateCustom(Customer entity) //actualizar un cliente
        {
            if (entity.IsValidated())
            {
                Update(entity);
                return true;
            }
            else
            {
                return false;
            }

        }

        public override void Delete(int id) //eliminar un cliente
        {
            Customer customer = em.Customer.Where(m => m.Id == id).First<Customer>();
            if (customer != null)
            {
                em.Customer.Remove(customer);
                em.SaveChanges();
            }
            else
                throw new ArgumentException("Cliente no encontrado");
        }

        public override List<Customer> GetAll() //obtener todos los clientes
        {
            List<Customer> customers = em.Customer.ToList<Customer>();
            return customers;
        }

        public override Customer GetEntity(int id) //obtener un cliente (un buscar)
        {
            return em.Customer.Where(m => m.Id == id).First<Customer>();
        }

        public Customer getCustomer()
        {
            Customer customer = new Customer();
            customer.Dni = "";
            customer.Lastname = "";
            customer.Name = "";
            customer.Phone = "";
            customer.Email = "";
            return customer;

        }
    }
}
