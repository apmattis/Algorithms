
import java.io.BufferedReader;
import java.io.BufferedWriter;
import java.io.FileNotFoundException;
import java.io.FileReader;
import java.io.FileWriter;
import java.io.IOException;
import java.io.PrintWriter;
import java.util.ArrayList;
import java.util.Collections;
import java.util.List;
import java.util.Random;
import java.util.Scanner;

/**
 *
 * @author Alex Mattis
 */
public class QuickSort {

    private static List<Integer> arrList;
    private static Random rand;
    private static int arrLength;

    public static void quicksort(List arr, int low, int high) {
        int partition;

        if (high - low <= 0) {
        } else {
            partition = partition(arr, low, high);
            quicksort(arr, low, partition - 1);
            quicksort(arr, partition + 1, high);
        }
    }

    public static int partition(List arr, int low, int high) {
        int left = low - 1;
        int right = high;
        int pivot = (int) arr.get(right);

        for (int j = low; j < right; j++) {
            if ((int) arr.get(j) <= pivot) {
                left = left + 1;
                Collections.swap(arr, left, j);
            }
        }
        Collections.swap(arr, left + 1, high);
        return left + 1;
    }

    public static void randomizedQuicksort(List arr, int low, int high) {
        int partition;

        if (high - low <= 0) {
        } else {
            partition = randomizedPartition(arr, low, high);
            randomizedQuicksort(arr, low, partition - 1);
            randomizedQuicksort(arr, partition + 1, high);
        }
    }

    public static int randomizedPartition(List arr, int low, int high) {
        int randomPivot;

        Random rand = new Random();
        randomPivot = rand.nextInt((high - low) + 1) + low;               //pick a random index from array
        Collections.swap(arr, randomPivot, high);                         //swap random index with high index

        return partition(arr, low, high);
    }

    public static void main(String[] args) {
        String line;
        String fin, fout;
        Scanner in = new Scanner(System.in);
        int j, choice, size;

        System.out.println("Enter 1 for random sized array or 2 for file input: ");
        choice = in.nextInt();
        
        if (choice == 1) {
            System.out.println("Enter the size of array to sort: ");
            size = in.nextInt();
            
            fout = "test/sorted_" + String.valueOf(size) + ".txt";
            
            rand = new Random();
            arrList = new ArrayList<>();
            
            for (int i = 0; i < size; i++) {
                int randomNum = rand.nextInt((150000 - 1 + 1)) + 1;
                arrList.add((int)randomNum);
            }
            
            List<Integer> arrCopy = arrList; 
            arrLength = arrList.size();

            long time1 = System.nanoTime();                 //start timer
            quicksort(arrCopy, 0, arrLength - 1);           //begin sort
            long totalTime = System.nanoTime() - time1;     //end timer

            System.out.println("Quicksort: " + totalTime / 1000 / 1000.0 + " ms\n");

            long time2 = System.nanoTime();
            randomizedQuicksort(arrCopy, 0, arrLength - 1);
            long totalTime2 = System.nanoTime() - time2;

            System.out.println("Randomized Quicksort: " + totalTime2 / 1000 / 1000.0 + " ms\n");

            try (FileWriter fWriter = new FileWriter(fout);
                    BufferedWriter bWriter = new BufferedWriter(fWriter)) {
                bWriter.write("Quicksort time: "
                        + String.valueOf(totalTime / 1000 / 1000.0) + " ms");
                bWriter.newLine();
                bWriter.write("Random Quicksort time: "
                        + String.valueOf(totalTime2 / 1000 / 1000.0) + " ms");
                bWriter.newLine();

                for (j = 0; j < arrList.size(); j++) {
                    bWriter.write(String.valueOf(arrCopy.get(j)));
                    bWriter.newLine();
                }

                bWriter.flush();
                bWriter.close();
            } catch (FileNotFoundException ex) {
                System.out.println("Unable to open file '"
                        + fout + "'");
            } catch (IOException ex) {
                System.out.println("Error reading file '"
                        + fout + "'");
            }

            try (FileWriter fWriter2 = new FileWriter("times.txt", true);
                    BufferedWriter bWriter2 = new BufferedWriter(fWriter2);
                    PrintWriter pwriter = new PrintWriter(bWriter2)) {
                pwriter.println(totalTime / 1000 / 1000.0 + "     |     " + totalTime2 / 1000 / 1000.0);
            } catch (IOException e) {
                System.out.println("Unable to open file times.txt");
            }     
            
        } else if (choice == 2) {
            System.out.println("Enter a filename: ");
            fin = "test/" + in.next();                          //user input

            fout = "test/sorted_" + fin.substring(5);           //add sorted prefix to out file

            //Read contents of file into arraylist
            try (FileReader fReader = new FileReader(fin);
                    BufferedReader bReader = new BufferedReader(fReader)) {
                arrList = new ArrayList<>();

                while ((line = bReader.readLine()) != null) {
                    arrList.add(Integer.parseInt(line));
                }

                bReader.close();
            } catch (FileNotFoundException ex) {
                System.out.println("Unable to open file '"
                        + fin + "'");
            } catch (IOException ex) {
                System.out.println("Error reading file '"
                        + fin + "'");
            }
            

            List<Integer> arrCopy2 = arrList;
            arrLength = arrList.size();

            long time1 = System.nanoTime();                 //start timer
            //quicksort(arrCopy2, 0, arrLength - 1);           //begin sort
            long totalTime = System.nanoTime() - time1;     //end timer

            System.out.println("Quicksort: " + totalTime / 1000 / 1000.0 + " ms\n");

            long time2 = System.nanoTime();
            randomizedQuicksort(arrCopy2, 0, arrLength - 1);
            long totalTime2 = System.nanoTime() - time2;

            System.out.println("Randomized Quicksort: " + totalTime2 / 1000 / 1000.0 + " ms\n");

            try (FileWriter fWriter = new FileWriter(fout);
                    BufferedWriter bWriter = new BufferedWriter(fWriter)) {
                bWriter.write("Quicksort time: "
                        + String.valueOf(totalTime / 1000 / 1000.0) + " ms");
                bWriter.newLine();
                bWriter.write("Random Quicksort time: "
                        + String.valueOf(totalTime2 / 1000 / 1000.0) + " ms");
                bWriter.newLine();

                for (j = 0; j < arrList.size(); j++) {
                    bWriter.write(String.valueOf(arrCopy2.get(j)));
                    bWriter.newLine();
                }

                bWriter.flush();
                bWriter.close();
            } catch (FileNotFoundException ex) {
                System.out.println("Unable to open file '"
                        + fout + "'");
            } catch (IOException ex) {
                System.out.println("Error reading file '"
                        + fout + "'");
            }

            try (FileWriter fWriter2 = new FileWriter("times.txt", true);
                    BufferedWriter bWriter2 = new BufferedWriter(fWriter2);
                    PrintWriter pwriter = new PrintWriter(bWriter2)) {
                pwriter.println(totalTime / 1000 / 1000.0 + "     |     " + totalTime2 / 1000 / 1000.0);
            } catch (IOException e) {
                System.out.println("Unable to open file times.txt");
            }
        }
    }

}
