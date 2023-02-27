﻿using System;
namespace Core.Helpers
{
	public static class PasswordHasher
	{
		public static string Encrypt(string password)
		{
			try
			{

				byte[] encDate_byte = new byte[password.Length];
				encDate_byte = System.Text.Encoding.UTF8.GetBytes(password);
				string encodeData = Convert.ToBase64String(encDate_byte);
				return encodeData;


			}
			catch (Exception ex)
			{

				throw new Exception("Error in base64Encode" + ex.Message);
			}



		}


		public static string Decrypt(string hashedPassword)

		{
			System.Text.UTF8Encoding encoder = new System.Text.UTF8Encoding();
			System.Text.Decoder utf8Decode = encoder.GetDecoder();
			byte[] todecode_byte = Convert.FromBase64String(hashedPassword);
			int charCount = utf8Decode.GetCharCount(todecode_byte, 0, todecode_byte.Length);
			char[] decoded_char = new char[charCount];
			utf8Decode.GetChars(todecode_byte, 0, todecode_byte.Length, decoded_char, 0);
			string result = new String(decoded_char);
			return result;


		}
	}
}

