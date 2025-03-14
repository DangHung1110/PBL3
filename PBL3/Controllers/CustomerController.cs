using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using PBL3.Service;
using System.Text.Json;


namespace PBL3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController:ControllerBase
    {
        private readonly CustomerService _customerservice;
        public CustomerController(CustomerService Customerservice)
        {
            _customerservice = Customerservice;
        }
        
       
    }



}
