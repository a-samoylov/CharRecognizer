## What is this?

This project is a neural network implementation that recognizes characters.

You can create and educate **your own neural network!**

## Getting Started

### Installing

Project writes on C# so you need .NET platform on your computer.

Clone the project and build it.

```
git clone https://github.com/a-samoylov/CharRecognizer
```

Configure App.config.

`path_to_data` is path where stored educate data for neural network.

`path_to_file_storage` is place where program will create report after every iteration educate network.

Example:

```xml
<?xml version="1.0" encoding="utf-8" ?>
<configuration>
    <startup> 
        <supportedRuntime version="v4.0" sku=".NETFramework,Version=v4.6.1" />
    </startup>
    <appSettings>
        <add key="path_to_data" value="F:\CharRecognizer\CharRecognizer\data"/>
        <add key="path_to_file_storage" value="F:\CharRecognizer\CharRecognizer\file_storage"/>
    </appSettings>
</configuration>
```

### Example of Educate

You can create own neural network. For example, I will create network with 3 layers (1600x100x10 neurons in layers).

**It neuron network for picture 40px * 40px and recognize chars 0-9.**

Network have already had random weights, so we can start educate it.
In educate process program will create reports, so you can understand how network educate.

![alt text](https://raw.githubusercontent.com/a-samoylov/CharRecognizer/master/Screenshots/Educate.png)

Report Example. We have unique identifier `ID` so we can see `Epoch` and `Error` with specific case.

![alt text](https://raw.githubusercontent.com/a-samoylov/CharRecognizer/master/Screenshots/Report.png)

Network has passed 500 epochs, we can try recognize something. Network recognize char `1` 
(because neuron `#2` has output data `0.86614`) and print output vector.

![alt text](https://raw.githubusercontent.com/a-samoylov/CharRecognizer/master/Screenshots/Recognize.png)


## Authors

Alexander Samoylov
> [LinkedIn](https://www.linkedin.com/in/alexander-samoylov/)

> [GitHub](https://github.com/a-samoylov)
