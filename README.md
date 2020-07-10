# GenomicSeqRetrieval

## Introduction
Genomic Sequence Retrival is a command line interface that provides search access to a collection of 16S DNA sequences. The program is primarily concerned with managing access to the to the collection. The first portion of the code (Levels 1-3) preform indexing, searching, and displaying of the information, using exact match searches. The second portion of the code (Levels 4-7) focuse on more complex search tasks and can preform inexact (partial match) searches. 

* The collection of 16S DNA Sequences is from the NCBI: “The National Center for Biotechnology Information is part of the 
United States National Library of Medicine, a branch of the National Institutes of Health. 
The NCBI is located in Bethesda…” (for more information visit https://www.ncbi.nlm.nih.gov/). 

* The 16S collection is provided in a large file named 16S.fasta. The format of the file is as follows: 
There are 20,767 DNA sequences in the file. Each sequence is represented by two lines. The first line begins with '>'
and describes the DNA's source, identity, and other valuable information. The second line contains the actual DNA query string.

* Example of the DNA lines: 
>NR_118941.1 Streptococcus alactolyticus strain NCDO 1091 16S ribosomal RNA, partial sequence
GAACGGGTGAGTAACGCGTAGGTAACCTGCCTTGTAGCGGGGGATAACTATTGGAAACGATAGCTAATACCGCATAACAG…
>NR_115365.1 Streptomyces albus strain CSSP327 16S ribosomal RNA, partial sequence
TCACGGAGAGTTTGATCCTGGCTCAGGACGAACGCTGGCGGCGTGCTTAACACATGCAAGTCGAACGATGAAGC…

## Build Intructions for Mac
The program was written in Visual Studio. Once the project folder, GenomicSeqenceRetrieval, is opened the solution (.sln) file can be found in the first visible folder GenomicSeqRe. The .cs and .exe files are located in the next folder GenomicSeq. Open command line prompt in the GenomicSequenceRetrieval file "cd GenomicSequenceRetrieval." For Mac you will need to create executable file. You can do this by writing the commands:"mcs GenomicSequenceRetrieval.cs" then click enter to turn the file into the executable version.

```bash
cd GenomicSequenceRetrieval
mcs GenomicSequenceRetrieval.cs
```
## Build Instructions for Windows
Once the project folder, GenomicSeqenceRetrieval, is opened the solution (.sln) file can be found in the first visible folder GenomicSeqRe. The .cs and .exe files are located in the next folder GenomicSeq. Open command line prompt inside the GenomicSeqRe folder. Compile the project using csc then run using the executable. 

```bash
csc GenomicSeqRetrieval.cs 
GenomicSeqRetrieval.exe -level1 16S.fasta 273 10
```
## Functionality

### 1) Sequential access using the files starting position.

* To preform use the following commands on the executable file: 
```bash
mono GenomicSequenceRetrieval.exe -level1 16s.fasta 273 10
```
* -level1 is the flag which indicates the type of search, 16s.fasta is the file to read from, 273 is the line number the output begins at and 10 is the number of sequences the program should output. 

* The program will then display the following output. See screen shot for exact formatting of DNA data:
![Sequence Results](https://github.com/chammett7/GenomicSeqRetrieval/blob/master/GenomicSeqRe/RunIntructions/1results.png)
![Seq Results2](https://github.com/chammett7/GenomicSeqRetrieval/blob/master/GenomicSeqRe/RunIntructions/1resultsC.png)

### 2) Find sequence by sequence-id

* To preform use the following commands on the executable file: 

```bash
mono GenomicSequenceRetrieval.exe -level2 16s.fasta NR_115365.1
```
* -level2 is the flag indicating the type of search, 16s.fasta is the file to read from, and NR_115365.1 is the sequence-id.  The program will respond by outputing the sequence lines of the item searched if the item is in the file. 

* Example of output: 
>NR_115365.1 Streptomyces albus strain CSSP327 16S ribosomal RNA, partial sequence
TCACGGAGAGTTTGATCCTGGCTCAGGACGAACGCTGGCGGCGTGCTTAACACATGCAAGTCGAACGATGAAGCCCTTCG…

### 3) Find a set of sequence-ids in a given query file then writes the output to the specified result file 

* To preform use the following commands on the executable file: 

```bash
mono GenomicSequenceRetrieval.exe -level3 16S.fasta query.txt  results.txt
```

* -level3 is the flag indicating the type of search, 16s.fasta is the file to read from, query.txt is the query file, and results.txt is the output file. The query.txt file contains a set of random sequence-ids. The program will compare query.txt with 16s.fasta then write out all the matching sequences to the results.txt file. If a sequence-id in the query.txt file is not in the 16s.fasta file it will not be printed to the results.txt file.

* Example of query.txt: 
>NR_115365.1
>NR_999999.9
>NR_118941.1

## IndexSequence16s Indexes the file and provides direct access to the sequences

#### 1)To index the fasta file go to the separte visual studio solution, IndexSequence16s.

* To preform use the following commands on the executable file: 

```bash
mono IndexSequence16s.exe 16S.fasta 16S.index
```

* The program will create a sequence-id index to the fasta file which supports direct access to sequences by sequence-id. Each line consists of a sequence-id and a file-offset. 

* Example of the index file: 
NR_115365.1 0
NR_999999.9 531
…
NR_118941.1 1236733
…

#### 2) Only create the index file once

### 4) Using the Index File for Direct Access to Sequences

* To preform use the following commands on the executable file: 

```bash
mono GenomicSequenceRetrieval.exe -level4 16S.fasta 16S.index query.txt results.txt
```
* -level4 is the flag indicating the type of search, 16s.fasta is the file to read from, 16S.index is the indexed file, query.txt contains a set of random sequence-ids, and results.txt is the output file. This will operate the same as Level3 by writing out all the matching sequences to the results.txt file except instead of using a sequential file scan it uses the index file. 

### 5) Search for an Exat Match of a DNA String 

* To preform use the following commands on the executable file: 

```bash
mono GenomicSequenceRetrieval.exe  -level5  16S.fasta CTGGTACGGTCAACTTGCTCTAAG
```
 
* -level5 is the flag indicating the type of search, 16s.fasta is the file to read from, and CTGGTACGGTCAACTTGCTCTAAG is the DNA query string. This will respond by displaying all matching sequence-ids to the console.

* Example of output: 
>NR_115365.1
>NR_123456.1
>NR_118941.1
>NR_432567.1
>NR_118900.1

### 6) Search for Sequence Data Containing a Given Word

* To preform use the following commands on the executable file: 

```bash
mono GenomicSequenceRetrieval.exe -level6 16S.fasta Streptomyces
```

* -level6 is the flag indicating the type of search, 16s.fasta is the file to read from, and Streptomyces is the meta-data word to be searched for. This will respond by displaying all matching sequence-ids to the console.

* Example of output: 
>NR_026530.1
>NR_026529.1
>NR_119348.1

*These sequence-ids correspond to sequences that match the word Streptomyces:
> NR_026530.1 Streptomyces macrosporus strain A1201 16S ribosomal RNA, partial sequence
GACGAACGCTGGCGGCGTGCTTAACACATGCAAGTCGAACGATGAACCTCCTTCGGGAGGGGATTAGTGGCGAACGGGTG…
>NR_026529.1 Streptomyces thermolineatus strain A1484 16S ribosomal RNA, partial sequence
GACGAACGCTGGCGGCGTGCTTAACACATGCAAGTCGAACGGTGAAGCCCTTCGGGGTGGATCAGTGGCGAACGGGTGAG…
>NR_119348.1 Streptomyces gougerotii strain DSM 40324 16S ribosomal RNA, partial sequence
AACGCTGGCGGCGTGCTTAACACATGCAAGTCGAACGATGAAGCCCTTCGGGGTGGATTAGTGGCGAACGGGTGAGTAAC…

### 7) Search for a Sequence Containing Wild Cards 

* To preform use the following commands on the executable file: 

```bash
mono GenomicSequenceRetrieval.exe -level7 16S.fasta  ACTG*GTAC*CA
```
-level7 is the flag indicating the type of search, 16s.fasta is the file to read from, and ACTG*GTAC*CA is the sequenced searched where the "*" stands for any amount of characters in the same position. The program will output all possible matches to the console.

* Example of output: 
>ACTGGTACCA
>ACTGCGTACCA
>ACTGGTACGCA
>ACTGAGTACTCA
>ACTGACGTACTGTGCCA
>ACTGACCGTACTGCA
>ACTGGTACTGTCA

* Regular expression was used to preform this level. 

## Author and Acknowledgment
The program was written by myself, Cecil Hammett (chammett7) for a major assignment in my Programming Principles unit at Queensland University of Technology. The unit took place in semester 2, 2019. Thank you cab201 teaching team for providing me with this assignment. Genomic Sequence Retrival was challenging but rewarding in the end.  
