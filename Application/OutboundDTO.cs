using System.Collections;

namespace Application
{
    public class OutboundDTO
    {
        private Hashtable Payload = new Hashtable();

        public OutboundDTO(){

        }

        public void AddField(DictionaryEntry newPair)
        {
            Payload.Add(newPair.Key, newPair.Value);
        }

        public Hashtable GetPayload()
        {
            return Payload;
        }
    }
}