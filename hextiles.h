#pragma once 




struct hex{
    int x;
    int y;
};


int abs_value(int value){
    if (value < 0){
        return value * -1;
    }
    else {
        return value;
    }
}


int Distance(hex first, hex second){
    return abs_value(first.x - second.x) + abs_value(first.y - second.y) / 2;
}