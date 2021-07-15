using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PaymentContext.Domain.Entities;
using PaymentContext.Domain.Enums;
using PaymentContext.Domain.ValueObjects;

namespace PaymentContext.Tests.Entities
{
    [TestClass]
    public class StudentsTests
    {
        private readonly Name _name;
        private readonly Document _document;
        private readonly Address _address;
        private readonly Email _email;
        private readonly Student _student;

        public StudentsTests()
        {
            _name = new Name("Bruce", "Wayne");
            _document = new Document("86636369035",  EDocumentType.CPF);
            _email = new Email("bruce_wayne@waynecorp.com");
            _address = new Address("Rua 5", "235", "Bairro Normal", "Gotham", "SP", "Brazil", "00000000");
            _student = new Student(_name, _document, _email);
        }


        [TestMethod]
        public void ShouldReturnErrorWhenHadActiveSubscribtion()
        {
            var subscription = new Subscription(null);
            var payment = new PayPalPayment("12345678", DateTime.Now, DateTime.Now.AddDays(5), 10, 10, "Wayne Corp", _document, _address, _email);
            subscription.AddPayment(payment);
            _student.AddSubscription(subscription);
            _student.AddSubscription(subscription);

            Assert.IsTrue(_student.Invalid);
        }

        [TestMethod]
        public void ShouldReturnErrorWhenSubscriptionHasNoPayment()
        {
            var subscription = new Subscription(null);
            _student.AddSubscription(subscription);
            Assert.IsTrue(_student.Invalid);
        }

        [TestMethod]
        public void ShouldReturnSuccessWhenAddSubscribtion()
        {
            var subscription = new Subscription(null);
            var payment = new PayPalPayment("12345678", DateTime.Now, DateTime.Now.AddDays(5), 10, 10, "Wayne Corp", _document, _address, _email);

            subscription.AddPayment(payment);
            
            _student.AddSubscription(subscription);

            Assert.IsTrue(_student.Valid);
        }
    }
}