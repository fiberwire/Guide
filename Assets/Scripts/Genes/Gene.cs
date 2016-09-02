using UnityEngine;
using System.Collections.Generic;
using System;


public class Gene {
    public Action apply;
}

/*
 * To add a new gene, extend this class, 
 * add a new word in Chromosome.words,
 * add a case for it in Chromosome.getGeneFromWord
 */ 


