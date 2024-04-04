using System.Security.Cryptography;

namespace MockOS {

    public class OSObject {

        private int _handle;
       

        public int Handle {
            get => _handle;
            set => _handle = value;
        }

        public int HashCode { get; set;
        }
        
        public override string ToString() {
            return $"{GetType().ToString()}" + " object";
        }

        public override int GetHashCode()
        {
           return this.HashCode;  

        }

        public OSObject() {
            SetHashCode();
        }

        private void  SetHashCode() {
             
                byte[] source = new byte[14];
                new Random().NextBytes(source);
                using (MD5 md5 = MD5.Create()) {
                    byte[] hashed = md5.ComputeHash(source);
                    this.HashCode = BitConverter.ToInt32(hashed,0);
                }
                  
            }
        }

       


    }
