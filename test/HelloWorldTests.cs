using System.Threading.Tasks;
using Google.Protobuf.WellKnownTypes;
using Shouldly;
using Xunit;

namespace AElf.Contracts.HelloWorld
{
    // This class is unit test class, and it inherit TestBase. Write your unit test code inside it
    public class HelloWorldTests : TestBase
    {
        [Fact]
        public async Task Update_ShouldUpdateMessageAndFireEvent()
        {
            // Arrange
            var inputValue = "Hello, World!";
            var input = new StringValue { Value = inputValue };

            // Act
            await HelloWorldStub.Update.SendAsync(input);

            // Assert
            var updatedMessage = await HelloWorldStub.Read.CallAsync(new Empty());
            updatedMessage.Value.ShouldBe(inputValue);
        }
        
        [Fact]
        public async Task Update_CountShouldBeAdded()
        {
            // Arrange
            var inputValue = "Hello, World!";
            var input = new StringValue { Value = inputValue };

            // Act
            await HelloWorldStub.Initialize.SendAsync(new Empty());
            await HelloWorldStub.Update.SendAsync(input);
            await HelloWorldStub.Read.CallAsync(new Empty());

            // Assert
            var count = await HelloWorldStub.GetCount.CallAsync(new Empty());
            count.Value.ShouldBe(1);
        }
    }
    
}