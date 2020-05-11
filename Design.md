##### Tyler Lucas                                                   
##### Created: Jan 21 2019

## Problem Description

Salvage stores are often found in a place where they need to research their own prices for their products. Salvage stores work
by buying bulk, outdated, and damaged goods from other stores. This leaves them with the problem of researching prices in a streamlined fashion.
The goal of this program is to calculate grocery store prices and query websites / website API's for price information. Another goal is to have the
calculator customizable to meet client needs.

## Requirements

The users will be salvage store employees researching grocery prices. The program must be customizable depending on the client needs such as: grocery
store percentages, grocery store names, and some sort of employee identification. There must be a database to store this information; ideally the database would not be a local file system so that multiple computers / programs can use the same database at one company.

There must be a calculator interface for the user, as well as buttons that represent each grocery store and their percentage. For instance, if Walmart
has a product for $7.99, then a button would have the name Walmart on it, and if it were clicked with the price $7.99, a percentage would be applied
to create the salvage store price e.g. maybe $4.99. Additionally, there must be different categories for these grocery stores. For example, Walmart
may have a Cooler, Freezer, Food, and Nonfood category.

The data required would be minimal. Most of the data storage would be grocery store information which includes percentages and names. There isn't a
current need for a price history or any other type of data storage.

## Proposed Design

For the GUI, the user will be able to enter in a UPC barcode via a scanner. If no price is found online from the web scraper, then there will be a collapsable calculator that will be revealed. It will have a regular calculator, as well as buttons corresponding to the client's customizable store buttons. Users will be able to enter in some kind of identifier, such as an ID number or name. For the database in the GUI, there will be an option to add, delete, or edit grocery store buttons for each category (cooler, freezer, food, etc.). This functionality should have some sort of admin privilege
so that any employee won't be able to edit the databse.

The project will leverage an MVVM design pattern. 

Currently, most of the project functionality resides in NFCalculatorViewModel.cs and NFCalculator.xaml/.xaml.cs.

As an additional note, the original design was sort of "Build one to throw away" mentality, however instead of
rebuilding the entire project, I think refactoring and then building from there will work better.