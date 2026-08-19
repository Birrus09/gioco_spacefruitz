#pragma once

#include <cmath>
#include <string>
#include <vector>
#include "random.h"
#include <iostream>
#include "hextiles.h"

#define PI 3.14159

//first tile on x axis is lower (even x coords, even y coords)

class domain;
class building;
class district;

class ray{
    public:
        float length;
        float rotation;
    };



class convolutionable_rays{
public:
    std::vector<ray> rays;


    void convolution(){
        std::vector<ray> conv_rays;
        for (int i = 0; i < rays.size(); i ++){
            if (i  == 0){
                ray r;
                r.length = (rays[i].length + rays[rays.size() - 1].length + rays[i + 1].length) / 3.0;
                conv_rays.push_back(r);
                ;
            }
            else if (i == rays.size()-1){
                ray r;
                r.length = (rays[i].length + rays[rays.size() - 1].length + rays[i - 1].length) / 3.0;
                conv_rays.push_back(r);
            }
            else{
                ray r;
                r.length = (rays[i].length + rays[i + 1].length + rays[i - 1].length) / 3.0;
                conv_rays.push_back(r);
            }
        }
        rays = conv_rays;
        std::cout << "convolution succesful" << std::endl; //just to make sure
    }

    void AddSineWave(float period){
        for (int i = 0; i < rays.size(); i++){
            rays[i].length += std::sin(i / period) * 0.5;
            if (rays[i].length > 1.0){
                rays[i].length = 1.0;
            }
        }       
    }
};

class cell {
public:
    int id;
    int x;
    int y;
    std::string resource = "None";
    bool isInCity;
    bool isOccupied;
    domain* domain;
    building* building;
    district* district;

    cell(int id, int x, int y) : id(id), x(x), y(y), isInCity(false), isOccupied(false), domain(nullptr), building(nullptr), district(nullptr) {}

    void GenerateResource(std::string domain_type){
        //TODO
        return;
    }


};

class district {
public:
    int id;
    std::string type;
    std::string name;
    std::string description;
    std::string owner;
    std::vector<cell*> cells;
};

class building {
public:
    int id;
    std::string type;
    std::string name;
    std::string description;
    std::string owner;
    cell* cell;
};

bool checkfunction();

class domain {
public:
    int id;
    int distance;
    int size = 0;
    std::string type;
    std::vector<std::vector<cell>> cells;


    void select_type(int &seed){
        int roll = (int)(randomnum(seed) * 7) + 1;
        if (roll < 4) {
            type = "asteroid";
        }
        else if (roll < 6) {
            type = "planet";
            if (distance < 2){
                type = "molten planet";
            }
            else if (distance < 4){
                type = "desert planet";
            }
            else if (distance < 7){
                roll = interval(1, 3, seed);
                if (roll == 1){
                    type = "continental planet";
                }
                else if (roll == 2){
                    type = "alpine planet";
                }
                else if (roll == 3){
                    type = "oceanic planet";
                }
            else{
                type = "frozen planet";
            }
            }
        }
        else {
            type = "gas planet";
        }
    }

    void generate_domain(int &seed){
        //max size 10, min size 5
        float iterations = 24.0;
        convolutionable_rays crays;

        for (float i = 0; i < 2*PI; i += 2*PI/iterations){
            ray r;
            r.length = intervalf(5.0, 10.0, seed);
            r.rotation = i;
            crays.rays.push_back(r);
        }
        crays.convolution();
        crays.AddSineWave(iterations);

        for (int i = 0; i < 10; i++){
            std::vector<cell> row;
            cells.push_back(row);
            for (int j = 0; j < 10; j++){
                //funzione per controllare se l'esagono con quelle coordinate è nell'area del poligono disegnato
                if (checkfunction()){
                    size++;
                    cell c(i * 10 + j, i, j);
                    c.GenerateResource(type);
                    row.push_back(c);
                }
            }
        }
    }
};

class star {
public:
    int id;
    std::string name;
    std::string type;
    std::vector<int> yield;
    int conjugate = 0; 
};

class S_System{
public:
    int id;
    star star;
    std::vector<int> rings; //example: {0, 0, 1, 1, 0, 0, 1, 1}
    std::vector<domain> domains; // from closest to furthest
};

struct node {
    std::vector<class S_System> systems;
};

class Map {
public:
    std::vector<std::vector<node>> nodes;
};




star CreateStar(int &seed, int id) {
    star s;
    s.id = id;
    s.name = "Star " + std::to_string(s.id);
    s.type = "Type " + std::to_string((int)(randomnum(seed)*7) + 1);

    if (chance(0.08, seed)) {
        s.conjugate = 1;
    }
    else {
        s.conjugate = 0;
    }

    return s;
}


S_System createSystem(int &seed, int &id) {
    S_System s;
    s.id = id++;
    int counter = 0;

    float rebalance = 0.1;
    for (int i = 0; i < 8; i++){

        if (chance(0.5 + rebalance, seed)) {
            s.rings.push_back(1);
            rebalance -= 0.05;
        }
        else {
            s.rings.push_back(0);
            rebalance += 0.07;
        }
    }
    for (int i = 0; i < 8; i++) {
        if (s.rings[i] == 1) {
            counter++;
        }
    }

    //debug
    std::cout << "system " << s.id << " has " << counter << " domains." << std::endl;

    s.star = CreateStar(seed, id);

    for (int i = 0; i < counter; i++) {
        if (s.rings[i]){
            domain d;
            d.distance = i;
            d.id = id++;
            d.select_type(seed);
            d.generate_domain(seed);
        }
    }
    return s;
}




node generate_Node(int &seed, int &id) {
    node n;
    int roll = (int)(randomnum(seed)*3) + 1;
    for (int i = 0; i < roll; i++) {
        S_System s;
        s = createSystem(seed, id);
        n.systems.push_back(s);
    }
    return n;
}




Map CreateMap(int seed, int density, int id = 0){
    int width = 25, height = 25;
    Map map;
    for (int x = 0; x < width; x++) {
        std::vector<node> row;
        for (int y = 0; y < height; y++) {
            node n = generate_Node(seed, id);
            row.push_back(n);
        }
        map.nodes.push_back(row);
    }
    return map;
}
