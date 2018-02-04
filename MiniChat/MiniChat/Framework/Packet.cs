using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.IO;
namespace MiniChat
{
    class Packet
    {
        ushort opc;
        public ushort Opcode
        {
            get
            {
                return opc;
            }
        }
        ushort size;
        public int Size
        {
            private set
            {
                size = (ushort)value;
            }
            get
            {
                return (int)size;
            }
        }
        private PacketWriter m_writer;
        private PacketReader m_reader;
        private byte[] buffer;
        public Packet(ushort opcode)
        {
            opc = opcode;
            m_writer = new PacketWriter();
            AddWORD(0);
            AddWORD(opcode);
        }
        public Packet(byte[] bytes,int startIndex)
        {
            buffer = bytes;
            m_writer = new PacketWriter(buffer);
            m_reader = new PacketReader(buffer);
            m_reader.BaseStream.Position = startIndex;
            m_writer.BaseStream.Position = startIndex;
            size = ReadWORD();
            opc = ReadWORD();
        }
        public byte[] GetBytes()
        {
            byte[] tempBuffer = ((MemoryStream)m_writer.BaseStream).ToArray();
            m_writer.BaseStream.Position = 0;
            m_writer.Write((ushort)m_writer.BaseStream.Length);
            return ((MemoryStream)m_writer.BaseStream).ToArray();
        }
        public Packet(Packet p)
        {
            opc = p.Opcode;
            Size = p.Size;
            m_writer = new PacketWriter(p.GetBytes());
            m_reader = new PacketReader(p.GetBytes());
        }
        public byte ReadBYTE()
        {
            return m_reader.ReadByte();
        }
        public byte[] ReadBYTE(int count)
        {
            return m_reader.ReadBytes(count);
        }
        public UInt16 ReadWORD()
        {
            return m_reader.ReadUInt16();
        }
        public UInt32 ReadDWORD()
        {
            return m_reader.ReadUInt32();
        }
        public UInt64 ReadQWORD()
        {
            return m_reader.ReadUInt64();
        }
        public string ReadString()
        {
            ushort count = ReadWORD();
            return ASCIIEncoding.Unicode.GetString(m_reader.ReadBytes(count * 2));
        }
        public void AddBYTE(byte value)
        {
            m_writer.Write(value);
        }
        public void AddWORD(UInt16 value)
        {
            m_writer.Write(value);
        }
        public void AddDWORD(UInt32 value)
        {
            m_writer.Write(value);
        }
        public void AddQWORD(UInt64 value)
        {
            m_writer.Write(value);
        }
        public void AddString(string value)
        {
            AddWORD((ushort)value.Length);
            m_writer.Write(ASCIIEncoding.Unicode.GetBytes(value));
        }
        public void Write(byte[] data)
        {
            m_writer.Write(data);
        }
    }
}
