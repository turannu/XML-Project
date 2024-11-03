## Sips & Bites Navigator
---
>
## Introduction
>Do you remember when was your last vacation? Do you know where to go for best Sips and Bites? Our Web app can help you choose a destination which provides information on active breweries, cideries, brewpubs, and bottleshops.
>
>- Provides you state codes for easy search by category  
>- Best Breweries
---
## Logo
>-![Screenshot 2024-11-03 122301](https://github.com/user-attachments/assets/e7e3dce8-badd-488f-a36f-8d08eb9c27f8)


## Data Feeds:-

>-https://www.openbrewerydb.org/

>-https://console.cloud.google.com/apis/library 

>-https://worldpopulationreview.com/static/states/abbr-name-list.json

### StoryBoard
---

>-![Storyboard](https://github.com/user-attachments/assets/ee1bb48c-fa26-4086-8a49-0534a7624594)

## Functional Requirements 1

**Scenerio**

As a user, I want to be able to search for and navigate for any sip-and-bites location around me, filtering based on preferences 

**Dependencies**

Navigation data are avaialble and accessible.

**Example**

1.1

Based on the available data

When I search for breweries

I should be able to see different varieties and then navigate through them

1.2

Given the data available

When I search for Bottleshops, 

Then I shoud be able to see locations that sell variety of alcoholic beverages,

such as beer, cider, wine, and sometimes spirits

**Search & Navigation** FR-01: Location Search
- System shall allow users to search breweries by state codes
- System shall provide a dropdown/list of valid state codes
- System shall must validate state code inputs
  
**Category Filtering** FR-02

### Functional Requirement 2:

#### Scenario

As a user, I want to filter breweries by their beer styles (Brewerier, Cideries ,Brewpubs, etc.), so that I can find breweries that make my preferred beer types.

- System shall allow filtering by establishment type
   -Breweries
   -Cideries
   -Brewpubs
   -Bottleshops
  
**Result Display** FR-03
- System shall display search result in an organized list/grid
- Each result must show:
   * Establishment name
   * Category/type
   * Address
   * Operating hours
   * Contact information
 
**Navigation and Accessibility** FR-04 Interactive Map
- System shall display establishment locations on a map
- Users must be able to get directions
- Map should show multiple locations

**Technical Requirements** FR-05 API Integration
- System shall integrate with brewery database API
- System shall maintain data Accuracy

## Scrum Rules
- Product Owner: Neetu Turan
- DevOps: Aliu Adekunle
- Integration Developer: Adenike Adeniran

## Weekly Team Members Meetings
Fridays (In person),
Saturdays (Teams).

 ## Weekly Meeting Time
 Fridays: 5:00pm to 6:00pm,
 Saturdays: 1:00pm to 2:00pm.
 
>  
