using FilaAzure.FolderProducer;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FilaAzure_Test
{
    [TestClass]
    public class ProducerSendTest
    {
        [TestMethod]
        [DataRow("Primeira mensagem aqui!")]
        [DataRow("Segunda mensagem aqui!")]
        [DataRow("Terceira mensagem aqui!")]
        public void SendMessageForQueue(string message) 
        {
            var result = new ProducerService().ProducingMessage(message);
            Assert.AreEqual(result.Result.OwnsConnection, true);
        }

    }
}
