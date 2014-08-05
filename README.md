APINet
======

Library for creating a .Net usable version of a Web API that uses a query string.

Class usage
===========

ApiSite - Class for building and searching desired site.

Example: Indeed API (From https://ads.indeed.com)

Base Url: http://api.indeed.com/ads/apisearch?

Request parameters: publisher, v (version), q (query), l (location).

Response in XML:

```xml
<?xml version="1.0" encoding="UTF-8" ?>
<response version="2">
    <query>java</query>
    <location>austin, tx</location>
    <dupefilter>true</dupefilter>
    <highlight>false</highlight>
    <totalresults>547</totalresults>
    <start>1</start>
    <end>10</end>
    <radius>25</radius>
    <pageNumber>0</pageNumber>
    <results>
        <result>
            <jobtitle>Java Developer</jobtitle>
            <company>XYZ Corp.</company>
            <city>Austin</city>
            <state>TX</state>
            <country>US</country>
            <formattedLocation>Austin, TX</formattedLocation>
            <source>Dice</source>
            <date>Mon, 02 Aug 2010 16:21:00 GMT</date>
            <snippet>looking for an object-oriented Java Developer... Java Servlets, HTML, JavaScript,
            AJAX, Struts, Struts2, JSF) desirable. Familiarity with Tomcat and the Java...</snippet>
            <url>http://www.indeed.com/viewjob?jk=12345&indpubnum=8343699265155203</url>
            <onmousedown>indeed_clk(this,'0000');</onmousedown>
            <latitude>30.27127</latitude>
            <longitude>-97.74103</longitude>
            <jobkey>12345</jobkey>
            <sponsored>false</sponsored>
            <expired>false</expired>
            <formattedLocationFull>Austin, TX</formattedLocationFull>
            <formattedRelativeTime>11 hours ago</formattedRelativeTime>
        </result>
    </results>
</response>
```

The desired data is in the _result_ node, the parents to it are _response_ and _results_.
The desired data per result is the result names: _jobtitle_, _company_, _formattedLocation_, and _snippet_.

Using this information, the class is created as follows:

Set up base url, argument names, response result names, and parent node names.

```C#
string baseUrl = "http://api.indeed.com/ads/apisearch?";
string[] argumentNames = new string[] { "publisher", "v", "q", "l" };
string[] responseNames = new string[] { "jobtitle", "company", "formattedLocation", "snippet" };
string[] parentNames = new string[] { "response", "results" };
string responseName = "result";
```

Instantiate the ApiSite.

```C#
ApiSite indeed = new ApiSite(baseUrl, argumentNames, responseNames);
indeed.ParentNodes = parentNames;
indeed.ResponseName = responseName;
```

Set argument values at needed.  This function returns the ApiSite so it can be chained.

```C#
indeed.SetArgumentValue("publisher", publisherKey).SetArgumentValue("v", "2");
```

Search and get a list of ApiResponseItems.

```C#
List<IndeedJob> jobList = new List<IndeedJob>();
List<ApiResponseItem> response = await indeed.Search();
foreach (ApiResponseItem item in response)
{
  jobList.Add(new IndeedJob
  {
    Title = item.GetSubItem("jobtitle"),
    Company = item.GetSubItem("company"),
    Location = item.GetSubItem("formattedLocation"),
    Description = item.GetSubItem("snippet")
  });
}
```

Enumerations can also be used to make it type-safe.

```C#
enum IndeedArguments {
  publisher, v, q, l
}

enum IndeedResponseItem {
  jobtitle, company, formattedLocation, snippet
}

class TypeSafeSearch {
  ApiSite<IndeedArguments, IndeedResponseItem> indeed;
  
  public TypeSafeSearch() {
    indeed = new ApiSite<IndeedArguments, IndeedResponseItem>(baseUrl);
    indeed.ParentNodes = parentNames;
    indeed.ResponseName = responseName;
  
    indeed.SetArgumentValue(IndeedArguments.publisher, publisherKey).SetArgumentValue(IndeedArguments.v, "2");
    
    List<IndeedJob> jobList = new List<IndeedJob>();
    List<ApiResponseItem<ResponseNames>> response = await _indeedApi.Search();

    foreach (ApiResponseItem<ResponseNames> item in response)
    {
      jobList.Add(new IndeedJob
      {
        Title = item.GetSubItem(ResponseNames.jobtitle),
        Company = item.GetSubItem(ResponseNames.company),
        Location = item.GetSubItem(ResponseNames.formattedLocation),
        Description = item.GetSubItem(ResponseNames.snippet)
      });
    }
  }
}
```
