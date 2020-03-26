﻿namespace TDSClient.TDS.Tokens
{
    using System;
    using System.IO;
    using System.Text;
    using TDSClient.TDS.Utilities;

    public class TDSInfoToken : TDSToken
    {
        /// <summary>
        /// Gets or sets information message number.
        /// </summary>
        public int Number { get; set; }

        /// <summary>
        /// Gets or sets information message state.
        /// </summary>
        public byte State { get; set; }

        /// <summary>
        /// Gets or sets information message class.
        /// </summary>
        public byte Class { get; set; }

        /// <summary>
        /// Gets or sets information message.
        /// </summary>
        public string MsgText { get; set; }

        /// <summary>
        /// Gets or sets server name.
        /// </summary>
        public string ServerName { get; set; }

        /// <summary>
        /// Gets or sets stored procedure name.
        /// </summary>
        public string ProcName { get; set; }

        /// <summary>
        /// Gets or sets line number.
        /// </summary>
        public uint LineNumber { get; set; }

        /// <summary>
        /// TDS Error Token Length
        /// </summary>
        /// <returns>Returns TDS Error Token Length</returns>
        public override ushort Length()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Used to pack IPackageable to a stream.
        /// </summary>
        /// <param name="stream">MemoryStream in which IPackageable is packet into.</param>
        public override void Pack(MemoryStream stream)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Used to unpack IPackageable from a stream.
        /// </summary>
        /// <param name="stream">MemoryStream from which to unpack IPackageable.</param>
        /// <returns>Returns true if successful.</returns>
        public override bool Unpack(MemoryStream stream)
        {
            LittleEndianUtilities.ReadUShort(stream);
            this.Number = (int)LittleEndianUtilities.ReadUInt(stream);
            this.State = Convert.ToByte(stream.ReadByte());
            this.Class = Convert.ToByte(stream.ReadByte());

            int length = LittleEndianUtilities.ReadUShort(stream) * 2;
            var buffer = new byte[length];
            stream.Read(buffer, 0, length);
            this.MsgText = Encoding.Unicode.GetString(buffer);

            length = stream.ReadByte() * 2;
            buffer = new byte[length];
            stream.Read(buffer, 0, length);
            this.ServerName = Encoding.Unicode.GetString(buffer);

            length = stream.ReadByte() * 2;
            buffer = new byte[length];
            stream.Read(buffer, 0, length);
            this.ProcName = Encoding.Unicode.GetString(buffer);

            this.LineNumber = LittleEndianUtilities.ReadUInt(stream);

            return true;
        }
    }
}
