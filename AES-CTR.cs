using System.Collections.Generic;using System.Security.Cryptography;public class Aes128CounterMode:SymmetricAlgorithm{private byte[]c;private AesManaged a;
public Aes128CounterMode(byte[]e){a=new AesManaged{Mode=CipherMode.ECB,Padding=PaddingMode.None};c=e;}public override ICryptoTransform CreateEncryptor
(byte[]k,byte[]n){return new l(a,k,c,true);}public override ICryptoTransform CreateDecryptor(byte[]k,byte[]n){return new l(a,k,c,false);}public override void
GenerateKey(){a.GenerateKey();}public override void GenerateIV(){}}public class l:ICryptoTransform{private byte[]c;private ICryptoTransform e;private Queue<byte>
x=new Queue<byte>();private SymmetricAlgorithm a;public l(SymmetricAlgorithm y,byte[]k,byte[]r,bool q){a=y;c=r;var z=new byte[a.BlockSize/8];if(q){e=y.CreateEncryptor
(k,z);}else{e=y.CreateDecryptor(k,z);};}public byte[]TransformFinalBlock(byte[]b,int o,int n){var t=new byte[n];TransformBlock(b,o,n,t,0);return t;}public int 
TransformBlock(byte[]b,int o,int n,byte[]r,int f){for(var i=0;i<n;i++){if(p())h();var m=x.Dequeue();r[f+i]=(byte)(b[o+i]^m);}return n;}private bool p()
{return x.Count==0;}private void h(){var l=new byte[a.BlockSize/8];e.TransformBlock(c,0,c.Length,l,0);u();foreach (var b in l){x.Enqueue(b);}}private void u()
{for(var i=c.Length-1;i>=0;i--){if(++c[i]!=0)break;}}public int InputBlockSize{get{return a.BlockSize/8;}}public int OutputBlockSize{get{return a.BlockSize/8;}}
public bool CanTransformMultipleBlocks{get{return true;}}public bool CanReuseTransform{get{return false;}}public void Dispose(){}}