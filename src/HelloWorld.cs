using AElf.Sdk.CSharp;
using Google.Protobuf.WellKnownTypes;

namespace AElf.Contracts.HelloWorld
{
    // Contract class must inherit the base class generated from the proto file
    public class HelloWorld : HelloWorldContainer.HelloWorldBase
    {
        // A method that modifies the contract state
        public override Empty Initialize(Empty input)
        {
            State.Count.Value = 0;
            return new Empty();
        }

        public override Empty Update(StringValue input)
        {
            // Set the message value in the contract state
            State.Message.Value = input.Value;
            State.Count.Value += 1;
            // Emit an event to notify listeners about something happened during the execution of this method
            Context.Fire(new UpdatedMessage
            {
                Value = input.Value
            });
            return new Empty();
        }

        // A method that read the contract state
        public override StringValue Read(Empty input)
        {
            // Retrieve the value from the state
            var value = State.Message.Value;
            // Wrap the value in the return type
            return new StringValue
            {
                Value = value
            };
        }

        public override Int64Value GetCount(Empty input)
        {
            return new Int64Value
            {
                Value = State.Count.Value
            };
        }
    }
    
}