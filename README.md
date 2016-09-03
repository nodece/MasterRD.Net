## Requirements
* .net framework 2.0
* Visual Studio 2015

## Installation 
* Clone
    ```
    git clone https://github.com/nodece/MasterRD.Net.git
    ```
* Use
    ```C#
    using MasterRD.Net;
    public void GetCard()
    {
        Rf rf = new Rf();
        string card = rf.ReadCard(1);
        Console.WriteLine(card); 
    }
    ```
    
## License
Code released under [the MIT license](http://opensource.org/licenses/MIT)

Copyright (c) 2016 Nodece

