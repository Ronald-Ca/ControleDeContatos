using Microsoft.AspNetCore.Mvc;

namespace ControleDeContatos.Controllers
{
    public class ContactController : Controller
    {
        // Dados estáticos simulando um banco de dados
        private static List<Contact> _contacts = new List<Contact>
        {
            new Contact { Id = 1, Name = "Ronald", Email = "ronald@gmail.com", Phone = "66 98406-6545" },
            new Contact { Id = 2, Name = "Jacob", Email = "jacob@gmail.com", Phone = "66 98470-9581" },
            new Contact { Id = 3, Name = "Larry", Email = "larry@gmail.com", Phone = "66 98406-9581" }
        };

        public IActionResult Index()
        {
            ViewBag.Contacts = _contacts;
            return View();
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(string name, string email, string phone)
        {
            if (!string.IsNullOrEmpty(name) && !string.IsNullOrEmpty(email))
            {
                var newId = _contacts.Count > 0 ? _contacts.Max(c => c.Id) + 1 : 1;
                var newContact = new Contact
                {
                    Id = newId,
                    Name = name,
                    Email = email,
                    Phone = phone
                };
                _contacts.Add(newContact);
            }
            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {
            var contact = _contacts.FirstOrDefault(c => c.Id == id);
            if (contact == null)
            {
                return RedirectToAction("Index");
            }
            ViewBag.Contact = contact;
            return View();
        }

        [HttpPost]
        public IActionResult Edit(int id, string name, string email, string phone)
        {
            var contact = _contacts.FirstOrDefault(c => c.Id == id);
            if (contact != null)
            {
                contact.Name = name;
                contact.Email = email;
                contact.Phone = phone;
            }
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            var contact = _contacts.FirstOrDefault(c => c.Id == id);
            if (contact == null)
            {
                return RedirectToAction("Index");
            }
            ViewBag.Contact = contact;
            return View();
        }

        [HttpPost]
        public IActionResult DeleteConfirmed(int id)
        {
            var contact = _contacts.FirstOrDefault(c => c.Id == id);
            if (contact != null)
            {
                _contacts.Remove(contact);
            }
            return RedirectToAction("Index");
        }
    }

    public class Contact
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public string Email { get; set; } = "";
        public string Phone { get; set; } = "";
    }
}
