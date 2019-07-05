# AgricoveBilling

Billing system designed for Green Agricove Ltd. 

Features:
- It allows generation of invoice, printing of invoice, and keeping a history of invoices. 
- It keeps track of items, item prices, customer details and provides autocomplete suggestions accordingly. 

There is no GST billing supprt or inventory management yet.

[Documentation (Wiki)](https://github.com/subhasiscgr/AgricoveBilling/wiki) (Incomplete)

# Download

###### Latest version: 0.7.84 Beta (External release). There is no stable release yet

Compiled binary can be downloaded here: [Download](http://www.mediafire.com/file/aqdht1i1rqgrylp/AgricoveBilling.exe/file)

# System Requirements
Windows 10 32bit with .NET 4.6.1

# Build Requirements
VS 2017 Community, Entity Framework Core, and Crystal Reports for Visual Studio

# Changelog

## [0.7.84] - 2019-07-06

- Fixed issues #9, #11, #13, #14
- Improved loading times

## [0.7.83] - 2019-07-05

- Fixed issues #5, #6, #7 

## [0.7.82] - 2019-07-05

- Fixed issue #4

## [0.7.81] - 2019-07-05

- Error message now visible over loading screen

## [0.7.6] - 2019-07-05 

- Optimised some database calls
- Added loading screen
- Bring window to front after loading

## [0.7.5] - 2019-07-03

- Shift Tab now working
- Ability to quantity in decimal
- Table cell color indicates field active/inactive state
- Quantity larger than 100 supported
- Unit price only resets when desc changes to known item
- Unit value no more shows null
- User can't edit disctype anymore
- Fixed tabindex
- % is now default in disctype
- Total amount paid is assumed to be full by default
- Gridview selected cell back color changed
- Search suggestions enabled in name and address fields
- due date now getting refreshed when new invoice is opened
- Auto highlight existing text in every field
- Paid can't be higher than due
- Desc lostfocus no more resets qty
- Paid amoutn change now runs on keydown
- Overall font size increase
- Form refresh now resets date fields
- currency now shows comma on textboxes and labels
- Close and minimise box positions changed
- Balance due should now update always
- Refresh button now resets gridview cell sizes
- Gridview row heights increased
- Gridview header size and header font size increased
- Gridview now supports multiline text
- Grirview now sorts by due
- Row color now indicates which row has due remaining
- Search by due date added
- Replace all decimal.parse with custom parse that supprts comma
- Numeric boxes are no mmore allowed to be blank
- Due can no more become negative
- Save button now gets enabled even when due is zero
- Print option moved to save dialogue box
- Save clears invoice
- New Invoice button now refresh button
- Print button now stays disabled unless data is being loaded from search
- Find button and shortcut stay disabled while user is editing invoice
- Due date can't be older than invoice date
- Old search gets reloaded when you return to search
- Item can now be edited
- Text on find button now dynamically changes
- Qty now supports multiple units
- Save and Print button now no more getting enabled in find view
- Deafult unit is now KG
- When piece is unit, qty doesn't allow decimals anymore
- Unit field added in CrystalReports
- Added catch to handle missing CrystalReports installation and missing rpt file
- recursive up completely redesigned
- Close button bug in gridview fixed
- Fields no more disabled when loading from history
- Editing old invoice no more loads or saves item value



