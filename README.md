# cryptocurrency-ticker

# Introduction

This is a WPF desktop application that you can run in the background, which will pull price data from 
Binance.com exchange API (within defined regular intervals) and presents them to the user. You can also elect to only show prices
if it is above a certain percentage change, as well as add an audio notification (which could be useful if you're a trader). 

# How to use
Using the application is very straigth forward, select your currency pair, percentage change and how often you want to pull 
the data by modifying the property values in the Details class. More details can be found on the [summary] in the 
repository.

# Unit tests
Yests are to follow shortly. Feel free to contribute.

# Prerequisite 
This was made on C# 7, Visual Studio 2019 version 16.6.4. Very little knowledge of C# is required to install and run. 
You will need a Binance exchange account to operate the applciation as it requires private keys.

# Acknowledgement
This application makes use of BinanceDotNet API implementation by glitch100.
