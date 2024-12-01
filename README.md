# Sips n Bites 
---
>
## Introduction
>Sips and Bites is your ultimate guide to an extraordinary culinary adventure. Here, you can explore a wide range of breweries and gastronomic delights, crafted to satisfy your palate and spark your curiosity. Whether you're a connoisseur of craft brews or an enthusiast of exquisite bites, Sips and Bites provides the perfect blend of taste and discovery. Dive into a world where every sip tells a story and every bite is a new experience. Welcome to a journey of flavors!
>
>- Provides you state codes for easy search by category  
>- Best Breweries
>- Best Restaurants
---
## Logo

> ![Sips and Bites Navigator logo letter color is white with a green background and says Sips and Bites](https://github.com/turannu/XML-Project/blob/master/wwwroot/Image/logo.png)
---

## Data Feeds

>- <Brewery Data Source>https://www.openbrewerydb.org/
>
>- <Restaurant api> https://raw.githubusercontent.com/turannu/IS-7012-NT/refs/heads/main/FastFoodRestaurants2.json
>
>- <US States Data>https://worldpopulationreview.com/static/states/abbr-name-list.json
---
## Project Board
> https://github.com/users/turannu/projects/2 <Github link to open Github Project with board task>
## StoryBoard


> ![Storyboard](https://github.com/user-attachments/assets/ee1bb48c-fa26-4086-8a49-0534a7624594)
---

## Functional Requirements 
> Provide users with the capability to search for breweries and restaurants .



#### Scenario:
> As a user interested in breweries and restaurant, I want to be able to search for breweries and restaurant based on any part of the name: brewery name,restaurant name, state, city,type so that I can find breweries and restaurant that match my interest.
#### Dependencies :

>- Brewery  data are avaialble and accessible.
>- Restaurant data are available and accessible
>- USA states data are available and accessible.

#### Assumptions :
> Breweries Name are in English
>
>  US states are in English
>
> Restaurant Name are in English

### Requirements 100.0: Search Breweries 
#### Examples 
>1.1
>  **Given** data of brewery is available  
>
>   **When**  I search for “Texas”  
>
>   **Then** I should receive at least one result with these attributes:  
>
>![Screenshot 2024-11-03 185611](https://github.com/user-attachments/assets/ec4735a0-4757-4be3-8e5f-d949d505c904)
>
>![Screenshot 2024-11-03 185805](https://github.com/user-attachments/assets/64567267-2424-4d93-93cc-19b4a4f9d938)
>---
>1.2
>   **Given** data of brewery is available  
>
>   **When**  I search for “brewpub”  
>
>   **Then** I should receive at least one result with these attributes:  
>![Screenshot 2024-11-03 190550](https://github.com/user-attachments/assets/b5eda1f0-9b47-40e5-8ccb-fe273cd35aae)
>---
>1.3  
> **Given**  data of brewery is available
>
> **When** I search for “sklujapouetllkjsda;u”
>
> **Then** I should receive zero results (an empty list)
>---
### Requirements 100.1: Search Restaurants:
>2.1
>**Given** data of restaurant is available
>
>**When**I  search for "MA"
>
>**Then** I should receive at least one result with these attributes: 
>-  "ID": 1,
>-   "address": "1284 Memorial Dr",
>-  "city": "Chicopee",
>-  "country": "US",
>-  "name": "Burger King",
>-  "province": "MA"
>
>2.2
>**Given**   data of Restaurant is available
>
>**After** selecting state ,I can also search restaurant by city .When I select for "Chicopee"
>
>**Then** I should receive at least one result with these attributes: 
>-  "ID": 1,
>-   "address": "1284 Memorial Dr",
>-  "city": "Chicopee",
>-  "country": "US",
>-  "name": "Burger King",
>-  "province": "MA"
>
### Requirements 100.3: Add Reviews:
>
>3.1
>**Given** Review Form is avilable
>
>**After** i can fill out the form to give review
>
>**Then** I can see my review data
>

---
## Technical Requirements
>- System shall integrate with brewery database and restaurant database API
>- 
>- System shall maintain data Accuracy
---
## Scrum Rules
>- Product Owner/Integration Developer: Neetu Turan
>- DevOps Engineer/Front-end Developer: Aliu Adekunle
>- Integration Developer/Back-End Developer : Adenike Adeniran
---
 ## Weekly Meeting Time and Format
 >- Fridays(In-Person): 5:00pm to 6:00pm
 >- Saturdays(Virtual): 1:00pm to 2:00pm
 ---

