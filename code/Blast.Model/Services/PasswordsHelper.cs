using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Blast.Model.Services
{
    /// <summary>
    /// Helper for password's related management
    /// </summary>
    public class PasswordsHelper
    {
        public const string AllowedSymbols = "!@#$%^&*()-_=+;:[{]}|,. <>";

        public enum PasswordScore
        {
            Blank = 0,
            VeryWeak = 1,
            Weak = 2,
            Medium = 3,
            Strong = 4,
            VeryStrong = 5
        }

        bool ContainsNumber (string password)
        {
            return password.Any(char.IsDigit);
        }

        bool ContainsLowercase(string password)
        {
            return password.Any(char.IsLower);
        }

        bool ContainsUppercase(string password)
        {
            return password.Any(char.IsUpper);
        }

        bool ContainsSymbols(string password)
        {
            return password.Any( (char a) =>
            {
                return AllowedSymbols.Contains(a);
            });
        }

        public PasswordScore CheckStrength(string password)
        {
            int score = 0;

            if (password==null || password.Length < 1)
                return PasswordScore.Blank;
            if (password.Length < 4)
                return PasswordScore.VeryWeak;

            if (password.Length >= 8)
                score++;
            if (password.Length >= 12)
                score++;
            if (ContainsLowercase(password))
                score++;
            if (ContainsUppercase(password))
                score++;
            if (ContainsSymbols(password))
                score++;

            return (PasswordScore)score;
        }
        
        public double BruteForceItearions(string password)
        {
            if (password == null)
                return 0;
            
            double multiplicationFactor = 0;

            if (ContainsLowercase(password))
                multiplicationFactor += 26;
            if (ContainsUppercase(password))
                multiplicationFactor += 26;
            if (ContainsSymbols(password))
                multiplicationFactor += AllowedSymbols.Length;
            if (ContainsNumber(password))
                multiplicationFactor += 10;

            double iterations=1;
            for (int i=0; i<password.Length; i++)
            {
                iterations *= multiplicationFactor;
            }

            return iterations;
        }

        public bool OnlyAllowedChars(string password)
        {
            if (password == null)
                return true;

            foreach (char c in password)
            {
                var cstr = c.ToString();

                if (!ContainsLowercase(cstr) &&
                    !ContainsUppercase(cstr) &&
                    !ContainsNumber(cstr) &&
                    !ContainsSymbols(cstr))
                    return false;
            }
            return true;
        }
    }
}
