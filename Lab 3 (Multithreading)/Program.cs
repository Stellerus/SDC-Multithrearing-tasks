using Lab_3__Multithreading_;

var processor = new BikeProcessor();

processor.SerializeBikes();

processor.MergeFiles();

processor.ReadResultFileSingleThread();

processor.ReadResultFileTwoThreads();

processor.ReadResultFileTenThreads();