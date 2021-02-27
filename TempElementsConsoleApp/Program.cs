using System;
using System.IO;
using TempElementsLib;

namespace TempElementsConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            using(var tempFile = new TempFile())
            {
                Console.WriteLine("Wciśnij dowolny klawisz po sprawdzeniu czy plik istnieje");
                Console.ReadKey();
                Console.WriteLine("Podaj tekst który ma zostać dodany do pliku.");
                var text = Console.ReadLine();
                tempFile.AddText(text);
                Console.WriteLine("Wciśnij dowolny klawisz aby przejść do usunięcia pliku");
                Console.ReadKey();
            }
            var tempFile2 = new TempFile($"{System.IO.Path.GetTempPath()}temp123.tmp");
            Console.WriteLine($"Before {tempFile2.FilePath} dispose");
            tempFile2.Dispose();
            tempFile2.AddText("2");

            try
            {
                Console.WriteLine("Podaj ścieżkę do tymczasowego pliku.");
                var tempFile4 = new TempFile(Console.ReadLine());
            }
            catch(DirectoryNotFoundException e)
            {
                Console.WriteLine("Ścieżka jest niepoprawna. Przynajmniej jeden z folderów nie istnieje.");
            }
            catch(ArgumentException e)
            {
                Console.WriteLine("Niepoprawna ścieżka");
            }

            using var tempFile3 = new TempFile();
            Console.WriteLine("Test c#8 using ");


            using var tempTxtFile = new TempTxtFile();
            Console.WriteLine("Wpisywanie do pliku lini test123");
            tempTxtFile.WriteLine("test123");
            Console.WriteLine("Wpisywanie do pliku test");
            tempTxtFile.Write("test");
            Console.WriteLine("Naciśnij dowolny klawisz gdy sprawdzisz już zawartość pliku.");
            Console.ReadKey();

            Console.WriteLine("Testowanie tworzenia folderu.");
            var tempDir = new TempDir();
            Console.WriteLine("Po utworzeniu folderu, przed dispose.");
            Console.WriteLine("Wciśnij dowolny klawisz aby usunąć folder.");
            Console.ReadKey();
            tempDir.Dispose();
            Console.WriteLine("Po dispose.");

            Console.WriteLine("Testowanie tworzenia listy.");
            using var tempList = new TempElementsList();
            Console.WriteLine("Dodanie pliku tymczasowego 1.");
            tempList.AddElement<TempFile>();
            Console.WriteLine("Dodanie pliku tekstowego 2.");
            tempList.AddElement<TempTxtFile>();
            Console.WriteLine("Dodanie pliku tymczasowego 3.");
            var tempListEl = tempList.AddElement<TempFile>();
            Console.WriteLine("Dodanie pliku tymczasowego 4.");
            var tempListEl2 = tempList.AddElement<TempFile>();
            Console.WriteLine("Testowanie listy - ilość elementów");
            Console.WriteLine(tempList.Elements.Count);
            Console.WriteLine("Czas na sprawdzenie czy pliki zostały utworzone, wciśnij dowolny klawisz po sprawdzeniu");
            Console.ReadKey();
            Console.WriteLine("Usuwanie 3 pliku za pomocą metody DeleteElement");
            tempList.DeleteElement(tempListEl);
            Console.WriteLine("Testowanie listy - ilość elementów");
            Console.WriteLine(tempList.Elements.Count);
            Console.WriteLine("Czas na sprawdzenie czy plik został usunięty, wciśnij dowolny klawisz po sprawdzeniu");
            Console.ReadKey();
            Console.WriteLine("Usuwanie 4 pliku za pomocą metody Dispose elementu");
            tempListEl2.Dispose();
            Console.WriteLine("Testowanie listy - ilość elementów");
            Console.WriteLine(tempList.Elements.Count);
            Console.WriteLine("Czas na sprawdzenie czy plik został usunięty, wciśnij dowolny klawisz po sprawdzeniu");
            Console.ReadKey();
            Console.WriteLine("Testowanie listy - ilość elementów po RemoveDestroyed");
            tempList.RemoveDestroyed();
            Console.WriteLine(tempList.Elements.Count);

            Console.WriteLine("Testowanie historii zmian");
            using ( var tempStack = new TempElementsStack()) {
                Console.WriteLine("Dodanie pliku txt 1");
                tempStack.AddElement<TempTxtFile>();
                Console.WriteLine("Dodanie pliku txt 2");
                tempStack.AddElement<TempTxtFile>();
                Console.WriteLine("Dodanie pliku txt 3");
                tempStack.AddElement<TempTxtFile>();
                Console.WriteLine("Dodanie pliku txt 4");
                tempStack.AddElement<TempTxtFile>();
                Console.WriteLine("Dodanie pliku txt 5");
                tempStack.AddElement<TempTxtFile>();
                Console.WriteLine("Dodanie pliku txt 6");
                tempStack.AddElement<TempTxtFile>();
                Console.WriteLine("Dodanie pliku txt 7");
                tempStack.AddElement<TempTxtFile>();
                Console.WriteLine("Dodanie pliku txt 8");
                tempStack.AddElement<TempTxtFile>();
                Console.WriteLine("Dodanie pliku txt 9");
                tempStack.AddElement<TempTxtFile>();
                Console.WriteLine("Dodanie pliku txt 10");
                tempStack.AddElement<TempTxtFile>();
                Console.WriteLine("Dodanie pliku txt 11");
                tempStack.AddElement<TempTxtFile>();
                Console.WriteLine("Dodanie pliku txt 12");
                tempStack.AddElement<TempTxtFile>();
                Console.WriteLine("Dodanie pliku txt 13");
                tempStack.AddElement<TempTxtFile>();
                Console.WriteLine("Limit wynosi 10, pliki 1,2,3 nie powinny istnieć.");
                Console.WriteLine(tempStack.Elements.Count);
                Console.WriteLine("Cofnięcie zmiany");
                var tempEl = tempStack.Pop<TempTxtFile>();
                Console.WriteLine("Nazwa pliku:");
                Console.WriteLine(tempEl.FilePath);
                Console.WriteLine("Ilość zapisanych zmian:");
                Console.WriteLine(tempStack.Elements.Count);
            }
        }
    }
}
