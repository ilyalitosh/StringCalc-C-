using System;
using System.Collections;
using System.Collections.Generic;

namespace Calculator
{
    public class StringCalc
    {
        private static ArrayList iterations = new ArrayList();
        private static ArrayList outputPolishString = new ArrayList();

        private static void ParseInput(String input)
        {
            iterations.Clear();
            outputPolishString.Clear();
            String buffer = "";
            for (int i = 0; i < input.Length; i++)
            {
                if ((input[i] != '(') && (input[i] != ')') && (input[i] != '/') && (input[i] != '*') && (input[i] != '+') && (input[i] != '-') && (input[i] != '^') && (input[i] != 's') && (input[i] != 'c') && (input[i] != 'l') && (input[i] != 'g') && (input[i] != 'n') && (input[i] != 'f') && (input[i] != 't'))
                {
                    buffer += input[i];
                }
                else
                {
                    switch (input[i])
                    {
                        case '(':                                                               //самый низкий приоритет "(",")"
                            iterations.Add(input[i]);
                            break;
                        case ')':
                            if (buffer != "")
                            {
                                iterations.Add(buffer);
                                buffer = "";
                            }
                            iterations.Add(input[i]);
                            break;
                        case '+':                                                               //низкий приоритет "+","-"
                            if (buffer != "")
                            {
                                iterations.Add(buffer);
                                buffer = "";
                            }
                            iterations.Add(input[i]);
                            break;
                        case '-':
                            if (buffer != "")
                            {
                                iterations.Add(buffer);
                                buffer = "";
                            }
                            if (i == 0)
                            {
                                iterations.Add("0");
                            }
                            else if (input[i - 1] == '(')
                            {
                                iterations.Add("0");
                            }
                            iterations.Add(input[i]);
                            break;
                        case '*':                                                               //высокий приоритет "*","/","sin","cos","lg","ln"
                            if (buffer != "")
                            {
                                iterations.Add(buffer);
                                buffer = "";
                            }
                            iterations.Add(input[i]);
                            break;
                        case '/':
                            if (buffer != "")
                            {
                                iterations.Add(buffer);
                                buffer = "";
                            }
                            iterations.Add(input[i]);
                            break;
                        case 's':
                            if (buffer != "")
                            {
                                iterations.Add(buffer);
                                buffer = "";
                            }
                            iterations.Add(input[i] + "i" + "n");
                            i += 2;
                            break;
                        case 'c':
                            if (buffer != "")
                            {
                                iterations.Add(buffer);
                                buffer = "";
                            }
                            iterations.Add(input[i] + "o" + "s");
                            i += 2;
                            break;
                        case 'g':
                            if (buffer != "")
                            {
                                iterations.Add(buffer);
                                buffer = "";
                            }
                            iterations.Add("l" + input[i]);
                            break;
                        case 'n':
                            if (buffer != "")
                            {
                                iterations.Add(buffer);
                                buffer = "";
                            }
                            iterations.Add("l" + input[i]);
                            break;
                        case 'f':
                            if (buffer != "")
                            {
                                iterations.Add(buffer);
                                buffer = "";
                            }
                            iterations.Add(input[i] + "a" + "c" + "t");
                            i += 3;
                            break;
                        case 't':
                            if (buffer != "")
                            {
                                iterations.Add(buffer);
                                buffer = "";
                            }
                            iterations.Add(input[i] + "a" + "n");
                            i += 2;
                            break;
                        case '^':                                                               //самый высокий приоритет "^"
                            if (buffer != "")
                            {
                                iterations.Add(buffer);
                                buffer = "";
                            }
                            iterations.Add(input[i]);
                            break;
                    }
                }
                if (i + 1 == input.Length)
                {
                    if (buffer != "")
                    {
                        iterations.Add(buffer);
                        buffer = "";
                    }
                }
            }
        }

        private static void IterationsToPolishString()
        {
            Stack<String> stack = new Stack<String>();
            for (int i = 0; i < iterations.Count; i++)
            {
                if ((iterations[i].ToString()[0] != '(') && (iterations[i].ToString()[0] != ')') && (iterations[i].ToString()[0] != '/') && (iterations[i].ToString()[0] != '*') && (iterations[i].ToString()[0] != '+') && (iterations[i].ToString()[0] != '-') && iterations[i].ToString()[0] != '^' && iterations[i].ToString()[0] != 's' && iterations[i].ToString()[0] != 'c' && iterations[i].ToString()[0] != 'l' && iterations[i].ToString()[0] != 'f' && iterations[i].ToString()[0] != 't')
                {
                    outputPolishString.Add(iterations[i]);
                }
                else
                {
                    switch (iterations[i].ToString())
                    {
                        case "(":
                            stack.Push(iterations[i].ToString());
                            break;
                        case ")":
                            while (!stack.Peek().Equals("("))
                            {
                                outputPolishString.Add(stack.Pop());
                            }
                            stack.Pop();
                            break;
                        case "+":
                            while (stack.Count != 0)
                            {
                                if (!stack.Peek().Equals("("))
                                {
                                    outputPolishString.Add(stack.Pop());
                                }
                                else
                                {
                                    break;
                                }
                            }
                            stack.Push(iterations[i].ToString());
                            break;
                        case "-":
                            while (stack.Count != 0)
                            {
                                if (!stack.Peek().Equals("("))
                                {
                                    outputPolishString.Add(stack.Pop());
                                }
                                else
                                {
                                    break;
                                }
                            }
                            stack.Push(iterations[i].ToString());
                            break;
                        case "*":
                            while (stack.Count != 0)
                            {
                                if ((!stack.Peek().Equals("(")) && (!stack.Peek().Equals("+")) && (!stack.Peek().Equals("-")))
                                {
                                    outputPolishString.Add(stack.Pop());
                                }
                                else
                                {
                                    break;
                                }
                            }
                            stack.Push(iterations[i].ToString());
                            break;
                        case "/":
                            while (stack.Count != 0)
                            {
                                if ((!stack.Peek().Equals("(")) && (!stack.Peek().Equals("+")) && (!stack.Peek().Equals("-")))
                                {
                                    outputPolishString.Add(stack.Pop());
                                }
                                else
                                {
                                    break;
                                }
                            }
                            stack.Push(iterations[i].ToString());
                            break;
                        case "sin":
                            while (stack.Count != 0)
                            {
                                if ((!stack.Peek().Equals("(")) && (!stack.Peek().Equals("+")) && (!stack.Peek().Equals("-")) && (!stack.Peek().Equals("*")) && (!stack.Peek().Equals("/")))
                                {
                                    outputPolishString.Add(stack.Pop());
                                }
                                else
                                {
                                    break;
                                }
                            }
                            stack.Push(iterations[i].ToString());
                            break;
                        case "cos":
                            while (stack.Count != 0)
                            {
                                if ((!stack.Peek().Equals("(")) && (!stack.Peek().Equals("+")) && (!stack.Peek().Equals("-")) && (!stack.Peek().Equals("*")) && (!stack.Peek().Equals("/")))
                                {
                                    outputPolishString.Add(stack.Pop());
                                }
                                else
                                {
                                    break;
                                }
                            }
                            stack.Push(iterations[i].ToString());
                            break;
                        case "lg":
                            while (stack.Count != 0)
                            {
                                if ((!stack.Peek().Equals("(")) && (!stack.Peek().Equals("+")) && (!stack.Peek().Equals("-")) && (!stack.Peek().Equals("*")) && (!stack.Peek().Equals("/")))
                                {
                                    outputPolishString.Add(stack.Pop());
                                }
                                else
                                {
                                    break;
                                }
                            }
                            stack.Push(iterations[i].ToString());
                            break;
                        case "ln":
                            while (stack.Count != 0)
                            {
                                if ((!stack.Peek().Equals("(")) && (!stack.Peek().Equals("+")) && (!stack.Peek().Equals("-")) && (!stack.Peek().Equals("*")) && (!stack.Peek().Equals("/")))
                                {
                                    outputPolishString.Add(stack.Pop());
                                }
                                else
                                {
                                    break;
                                }
                            }
                            stack.Push(iterations[i].ToString());
                            break;
                        case "fact":
                            while (stack.Count != 0)
                            {
                                if ((!stack.Peek().Equals("(")) && (!stack.Peek().Equals("+")) && (!stack.Peek().Equals("-")) && (!stack.Peek().Equals("*")) && (!stack.Peek().Equals("/")))
                                {
                                    outputPolishString.Add(stack.Pop());
                                }
                                else
                                {
                                    break;
                                }
                            }
                            stack.Push(iterations[i].ToString());
                            break;
                        case "tan":
                            while (stack.Count != 0)
                            {
                                if ((!stack.Peek().Equals("(")) && (!stack.Peek().Equals("+")) && (!stack.Peek().Equals("-")) && (!stack.Peek().Equals("*")) && (!stack.Peek().Equals("/")))
                                {
                                    outputPolishString.Add(stack.Pop());
                                }
                                else
                                {
                                    break;
                                }
                            }
                            stack.Push(iterations[i].ToString());
                            break;
                        case "^":
                            while (stack.Count != 0)
                            {
                                if (stack.Peek().Equals("^"))
                                {
                                    outputPolishString.Add(stack.Pop());
                                }
                                else
                                {
                                    break;
                                }
                            }
                            stack.Push(iterations[i].ToString());
                            break;
                    }
                }
            }
            if (stack.Count != 0)
            {
                while (stack.Count != 0)
                {
                    outputPolishString.Add(stack.Pop());
                }
            }
        }

        public static double GetResult(String input)
        {
            ParseInput(input);
            IterationsToPolishString();
            Stack<Double> stack = new Stack<Double>();
            for (int i = 0; i < outputPolishString.Count; i++)
            {
                if ((outputPolishString[i].ToString()[0] != '(') && (outputPolishString[i].ToString()[0] != ')') && (outputPolishString[i].ToString()[0] != '/') && (outputPolishString[i].ToString()[0] != '*') && (outputPolishString[i].ToString()[0] != '+') && (outputPolishString[i].ToString()[0] != '-') && outputPolishString[i].ToString()[0] != '^' && outputPolishString[i].ToString()[0] != 's' && outputPolishString[i].ToString()[0] != 'c' && outputPolishString[i].ToString()[0] != 'l' && outputPolishString[i].ToString()[0] != 'f' && outputPolishString[i].ToString()[0] != 't')
                {
                    stack.Push(Double.Parse(outputPolishString[i].ToString()));
                }
                else
                {
                    double outValue1, outValue2;
                    switch (outputPolishString[i])
                    {
                        case "+":
                            outValue1 = stack.Pop();
                            outValue2 = stack.Pop();
                            stack.Push(outValue2 + outValue1);
                            break;
                        case "-":
                            outValue1 = stack.Pop();
                            outValue2 = stack.Pop();
                            stack.Push(outValue2 - outValue1);
                            break;
                        case "*":
                            outValue1 = stack.Pop();
                            outValue2 = stack.Pop();
                            stack.Push(outValue2 * outValue1);
                            break;
                        case "/":
                            outValue1 = stack.Pop();
                            outValue2 = stack.Pop();
                            stack.Push(outValue2 / outValue1);
                            break;
                        case "sin":
                            outValue1 = stack.Pop();
                            stack.Push(Math.Sin(outValue1 * (Math.PI / 180)));
                            break;
                        case "cos":
                            outValue1 = stack.Pop();
                            stack.Push(Math.Cos(outValue1 * (Math.PI / 180)));
                            break;
                        case "lg":
                            outValue1 = stack.Pop();
                            stack.Push(Math.Log10(outValue1));
                            break;
                        case "ln":
                            outValue1 = stack.Pop();
                            stack.Push(Math.Log(outValue1));
                            break;
                        case "fact":
                            outValue1 = stack.Pop();
                            stack.Push(Fact((int)outValue1));
                            break;
                        case "tan":
                            outValue1 = stack.Pop();
                            stack.Push(Math.Tan(outValue1 * (Math.PI / 180)));
                            break;
                        case "^":
                            outValue1 = stack.Pop();
                            outValue2 = stack.Pop();
                            stack.Push(Math.Pow(outValue2, outValue1));
                            break;
                    }
                }
            }

            return stack.Pop();
        }

        private static double Fact(int value)
        {                                             //расчет факториала
            if (value == 1)
            {
                return 1;
            }
            return Fact(value - 1) * value;
        }
    }
}

