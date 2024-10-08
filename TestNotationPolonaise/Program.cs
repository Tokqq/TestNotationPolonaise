using System;
using System.Collections.Generic;

namespace TestNotationPolonaise
{
    class Program
    {
        /// <summary>
        /// saisie d'une réponse d'un caractère parmi 2
        /// </summary>
        /// <param name="message">message à afficher</param>
        /// <param name="carac1">premier caractère possible</param>
        /// <param name="carac2">second caractère possible</param>
        /// <returns>caractère saisi</returns>
        static char saisie(string message, char carac1, char carac2)
        {
            char reponse;
            do
            {
                Console.WriteLine();
                Console.Write(message + " (" + char.ToUpper(carac1) + "/" + char.ToUpper(carac2) + ") ");
                reponse = Console.ReadKey().KeyChar;
                reponse = char.ToUpper(reponse);  // Convertir en majuscule pour comparaison uniforme
                Console.WriteLine();  // Passer à la ligne après avoir appuyé sur une touche
            } while (reponse != char.ToUpper(carac1) && reponse != char.ToUpper(carac2));
            return reponse;
        }

        /// <summary>
        /// Calcule une expression en notation polonaise
        /// </summary>
        /// <param name="expression">la formule en notation polonaise</param>
        /// <returns>résultat du calcul</returns>
        static double Polonaise(string expression)
        {
            string[] tokens = expression.Split(' ');
            Stack<double> stack = new Stack<double>();

            // On itère sur les éléments de droite à gauche
            for (int i = tokens.Length - 1; i >= 0; i--)
            {
                string token = tokens[i];

                if (double.TryParse(token, out double number))
                {
                    // Si le token est un nombre, on le place dans la pile
                    stack.Push(number);
                }
                else
                {
                    // Sinon, il doit s'agir d'un opérateur (+, -, *, /)
                    double operand1 = stack.Pop();
                    double operand2 = stack.Pop();

                    switch (token)
                    {
                        case "+":
                            stack.Push(operand1 + operand2);
                            break;
                        case "-":
                            stack.Push(operand1 - operand2);
                            break;
                        case "*":
                            stack.Push(operand1 * operand2);
                            break;
                        case "/":
                            stack.Push(operand1 / operand2);
                            break;
                        default:
                            throw new InvalidOperationException("Opérateur non valide : " + token);
                    }
                }
            }

            // Le dernier élément dans la pile est le résultat final
            return stack.Pop();
        }

        /// <summary>
        /// Saisie de formules en notation polonaise pour tester la fonction de calcul
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            char reponse;
            // boucle sur la saisie de formules
            do
            {
                Console.WriteLine();
                Console.WriteLine("Entrez une formule polonaise en séparant chaque partie par un espace = ");
                string laFormule = Console.ReadLine();
                // affichage du résultat
                Console.WriteLine("Résultat =  " + Polonaise(laFormule));
                reponse = saisie("Voulez-vous continuer ?", 'O', 'N');
            } while (reponse == 'O');
        }
    }
}
