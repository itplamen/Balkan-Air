<%@ Page Title="Developers" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Developers.aspx.cs" Inherits="BalkanAir.Web.Developers" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h1>API Overview</h1>

    <p>You are a pioneer of our new API. If you find a mistake, or can't find what you're looking for, let us know.</p>

    <ol type="i" id="ApiOverviewOrderedList">
        <li><a href="#AircraftManufacturers">Aircraft Manufacturers</a></li>
        <li><a href="#Aircrafts">Aircrafts</a></li>
        <li><a href="#Airports">Airports</a></li>
        <li><a href="#Countries">Countries</a></li>
        <li><a href="#Fares">Fares</a></li>
        <li><a href="#Flights">Flights</a></li>
        <li><a href="#News">News</a></li>
        <li><a href="#NewsCategories">News Categories</a></li>
        <li><a href="#Routes">Routes</a></li>
        <li><a href="#TravelClasses">Travel Classes</a></li>
        <li><a href="#Users">Users</a></li>
    </ol>

    <div id="AircraftManufacturers" class="apiDiv">
        <h2>Aircraft Manufacturers</h2>
        <hr />

        <h3>Request URIs</h3>
        <pre><code>GET /Api/AircraftManufacturers<br />GET /Api/AircraftManufacturers/{id}</code></pre>

        <table class="apiTableInfo">
            <thead>
                <tr>
                    <th>Variable</th>
                    <th>Description</th>
                    <th>Format</th>
                </tr>
            </thead>
            <tbody>
                <tr>
                    <td class="variableTd">{id}</td>
                    <td>Optionally return only this aircraft manufacturer.</td>
                    <td>Integer, e.g 44</td>
                </tr>
            </tbody>
        </table>

        <h3>Request Examples</h3>
        <pre><code>GET /Api/AircraftManufacturers<br />GET /Api/AircraftManufacturers/23</code></pre>
    </div>

    <div id="Aircrafts" class="apiDiv">
        <h2>Aircrafts</h2>
        <hr />

        <h3>Request URIs</h3>
        <pre><code>GET /Api/Aircrafts<br />GET /Api/Aircrafts/{id}</code></pre>

        <table class="apiTableInfo">
            <thead>
                <tr>
                    <th>Variable</th>
                    <th>Description</th>
                    <th>Format</th>
                </tr>
            </thead>
            <tbody>
                <tr>
                    <td class="variableTd">{id}</td>
                    <td>Optionally return only this aircraft.</td>
                    <td>Integer, e.g 44</td>
                </tr>
            </tbody>
        </table>

        <h3>Request Examples</h3>
        <pre><code>GET /Api/Aircrafts<br />GET /Api/Aircrafts/23</code></pre>
    </div>

    <div id="Airports" class="apiDiv">
        <h2>Airports</h2>
        <hr />

        <h3>Request URIs</h3>
        <pre><code>GET /Api/Airports<br />GET /Api/Airports/{id}<br />GET /Api/Airports/{abbreviation}</code></pre>

        <table class="apiTableInfo">
            <thead>
                <tr>
                    <th>Variable</th>
                    <th>Description</th>
                    <th>Format</th>
                </tr>
            </thead>
            <tbody>
                <tr>
                    <td class="variableTd">{id}</td>
                    <td>Optionally return only this airport.</td>
                    <td>Integer, e.g 44</td>
                </tr>
                <tr>
                    <td class="variableTd">{abbreviation}</td>
                    <td>Optionally return only this airport.</td>
                    <td>2-letter or 3-letter abbreviation, e.g SOF</td>
                </tr>
            </tbody>
        </table>

        <h3>Request Examples</h3>
        <pre><code>GET /Api/Airports<br />GET /Api/Airports/23<br />GET /Api/Airports/SOF</code></pre>
    </div>

    <div id="Countries" class="apiDiv">
        <h2>Countries</h2>
        <hr />

        <h3>Request URIs</h3>
        <pre><code>GET /Api/Countries<br />GET /Api/Countries/{id}<br />GET /Api/Countries/{abbreviation}</code></pre>

        <table class="apiTableInfo">
            <thead>
                <tr>
                    <th>Variable</th>
                    <th>Description</th>
                    <th>Format</th>
                </tr>
            </thead>
            <tbody>
                <tr>
                    <td class="variableTd">{id}</td>
                    <td>Optionally return only this country.</td>
                    <td>Integer, e.g 44</td>
                </tr>
                <tr>
                    <td class="variableTd">{abbreviation}</td>
                    <td>Optionally return only this country.</td>
                    <td>2-letter or 3-letter abbreviation, e.g BG</td>
                </tr>
            </tbody>
        </table>

        <h3>Request Examples</h3>
        <pre><code>GET /Api/Countries<br />GET /Api/Countries/23<br />GET /Api/Countries/BG</code></pre>
    </div>

    <div id="Fares" class="apiDiv">
        <h2>Fares</h2>
        <hr />

        <h3>Request URIs</h3>
        <pre><code>GET /Api/Fares<br />GET /Api/Fares/{id}<br />GET /Api/Fares/{originAbbreviation}/{destinationAbbreviation}</code></pre>

        <table class="apiTableInfo">
            <thead>
                <tr>
                    <th>Variable</th>
                    <th>Description</th>
                    <th>Format</th>
                </tr>
            </thead>
            <tbody>
                <tr>
                    <td class="variableTd">{id}</td>
                    <td>Optionally return only this fare.</td>
                    <td>Integer, e.g 44</td>
                </tr>
                <tr>
                    <td class="variableTd">{originAbbreviation}</td>
                    <td>Departure airport.</td>
                    <td>3-letter abbreviation, e.g SOF</td>
                </tr>
                <tr>
                    <td class="variableTd">{destinationAbbreviation}</td>
                    <td>Arrival airport.</td>
                    <td>3-letter abbreviation, e.g MAD</td>
                </tr>
            </tbody>
        </table>

        <h3>Request Examples</h3>
        <pre><code>GET /Api/Fares<br />GET /Api/Fares/23<br />GET /Api/Fares/SOF/MAD</code></pre>
    </div>

    <div id="Flights" class="apiDiv">
        <h2>Flights</h2>
        <hr />

        <h3>Request URIs</h3>
        <pre><code>GET /Api/Flights<br />GET /Api/Flights/{id}<br />GET /Api/Flights/{flightNumber}<br />GET /Api/Flights/Status/{flightStatus}<br />GET /Api/Flights/Departures/{airportAbbreviation}<br />GET /Api/Flights/Arrivals/{airportAbbreviation}<br />GET /Api/Flights/Route/{originAbbreviation}/{destinationAbbreviation}<br />GET /Api/Flights/DateTime/{dateTime}</code></pre>

        <table class="apiTableInfo">
            <thead>
                <tr>
                    <th>Variable</th>
                    <th>Description</th>
                    <th>Format</th>
                </tr>
            </thead>
            <tbody>
                <tr>
                    <td class="variableTd">{id}</td>
                    <td>Optionally return only this flight.</td>
                    <td>Integer, e.g 44</td>
                </tr>
                <tr>
                    <td class="variableTd">{flightNumber}</td>
                    <td>Optionally return all flights with this number.</td>
                    <td>String, e.g FR9Y24</td>
                </tr>
                <tr>
                    <td class="variableTd">{flightStatus}</td>
                    <td>Retrieve all flights with this status.</td>
                    <td>String, e.g Check-in</td>
                </tr>
                <tr>
                    <td class="variableTd">/Departures/{airportAbbreviation}</td>
                    <td>Retrieve all flights from this airport.</td>
                    <td>3-letter abbreviation, e.g SOF</td>
                </tr>
                <tr>
                    <td class="variableTd">/Arrivals/{airportAbbreviation}</td>
                    <td>Retrieve all flights to this airport.</td>
                    <td>3-letter abbreviation, e.g MAD</td>
                </tr>
                <tr>
                    <td class="variableTd">/Route/{originAbbreviation}/{destinationAbbreviation}</td>
                    <td>Retrieve all flights with this route.</td>
                    <td>3-letter abbreviation, e.g /SOF/MAD</td>
                </tr>
                <tr>
                    <td class="variableTd">{dateTime}</td>
                    <td>Retrieve all flights departing at this date and time.</td>
                    <td>yyyy-MM-ddTHH:mm, e.g 2017-04-02T17:30</td>
                </tr>
            </tbody>
        </table>

        <h3>Request Examples</h3>
        <pre><code>GET /Api/Flights<br />GET /Api/Flights/23<br />GET /Api/Flights/FR9Y24<br />GET /Api/Flights/Status/Check-in<br />GET /Api/Flights/Departures/SOF<br />GET /Api/Flights/Arrivals/MAD<br />GET /Api/Flights/Route//SOF/MAD<br />GET /Api/Flights/DateTime/2017-04-02T17:30</code></pre>
    </div>

    <div id="News" class="apiDiv">
        <h2>News</h2>
        <hr />

        <h3>Request URIs</h3>
        <pre><code>GET /Api/News<br />GET /Api/News/{id}<br />GET /Api/News/Latest/{count}<br />GET /Api/News/Latest/{count}/{category}</code></pre>

        <table class="apiTableInfo">
            <thead>
                <tr>
                    <th>Variable</th>
                    <th>Description</th>
                    <th>Format</th>
                </tr>
            </thead>
            <tbody>
                <tr>
                    <td class="variableTd">{id}</td>
                    <td>Optionally return only this news.</td>
                    <td>Integer, e.g 44</td>
                </tr>
                <tr>
                    <td class="variableTd">{count}</td>
                    <td>Optionally return latest 5 news, if {count} is not set and not combined with {category}.
                        Required when is combined  with {category}.
                    </td>
                    <td>Integer, e.g 3</td>
                </tr>
                <tr>
                    <td class="variableTd">{category}</td>
                    <td>Retrieve number of news, depending of {count} value, with this category.
                    </td>
                    <td>String, e.g /3/Economy</td>
                </tr>
            </tbody>
        </table>

        <h3>Request Examples</h3>
        <pre><code>GET /Api/News<br />GET /Api/News/23<br />GET /Api/News/Latest<br />GET /Api/News/Latest/3<br />GET /Api/News/Latest3/Economy</code></pre>
    </div>

    <div id="NewsCategories" class="apiDiv">
        <h2>News Categories</h2>
        <hr />

        <h3>Request URIs</h3>
        <pre><code>GET /Api/Categories<br />GET /Api/Categories/{id}<br />GET /Api/Categories/{categoryName}</code></pre>

        <table class="apiTableInfo">
            <thead>
                <tr>
                    <th>Variable</th>
                    <th>Description</th>
                    <th>Format</th>
                </tr>
            </thead>
            <tbody>
                <tr>
                    <td class="variableTd">{id}</td>
                    <td>Optionally return only this category.</td>
                    <td>Integer, e.g 44</td>
                </tr>
                <tr>
                    <td class="variableTd">{categoryName}</td>
                    <td>Optionally return only this category and all news related news.</td>
                    <td>String, e.g Routes</td>
                </tr>
            </tbody>
        </table>

        <h3>Request Examples</h3>
        <pre><code>GET /Api/Categories<br />GET /Api/Categories/23<br />GET /Api/Categories/Routes</code></pre>
    </div>

    <div id="Routes" class="apiDiv">
        <h2>Routes</h2>
        <hr />

        <h3>Request URIs</h3>
        <pre><code>GET /Api/Routes<br />GET /Api/Routes/{id}</code></pre>

        <table class="apiTableInfo">
            <thead>
                <tr>
                    <th>Variable</th>
                    <th>Description</th>
                    <th>Format</th>
                </tr>
            </thead>
            <tbody>
                <tr>
                    <td class="variableTd">{id}</td>
                    <td>Optionally return only this route.</td>
                    <td>Integer, e.g 44</td>
                </tr>
            </tbody>
        </table>

        <h3>Request Examples</h3>
        <pre><code>GET /Api/Routes<br />GET /Api/Routes/23</code></pre>
    </div>

    <div id="TravelClasses" class="apiDiv">
        <h2>Travel Classes</h2>
        <hr />

        <h3>Request URIs</h3>
        <pre><code>GET /Api/TravelClasses<br />GET /Api/TravelClasses/{id}<br />GET /Api/TravelClasses/Type/{type}<br />GET /Api/TravelClasses/AircraftId/{aircraftId}</code></pre>

        <table class="apiTableInfo">
            <thead>
                <tr>
                    <th>Variable</th>
                    <th>Description</th>
                    <th>Format</th>
                </tr>
            </thead>
            <tbody>
                <tr>
                    <td class="variableTd">{id}</td>
                    <td>Optionally return only this travel class.</td>
                    <td>Integer, e.g 44</td>
                </tr>
                <tr>
                    <td class="variableTd">{type}</td>
                    <td>Retrieve all travel classes with this type.</td>
                    <td>String, possible types: First, Business, Economy</td>
                </tr>
                <tr>
                    <td class="variableTd">{aircraftId}</td>
                    <td>Retrieve all travel classes with this aircraft id.</td>
                    <td>Integer, e.g 3</td>
                </tr>
            </tbody>
        </table>

        <h3>Request Examples</h3>
        <pre><code>GET /Api/TravelClasses<br />GET /Api/TravelClasses/23<br />GET /Api/TravelClasses/Type/Business<br />GET /Api/TravelClasses/AircraftId/3</code></pre>
    </div>

    <div id="Users" class="apiDiv">
        <h2>Users</h2>
        <hr />

        <h3>Request URIs</h3>
        <pre><code>GET /Api/Users<br />GET /Api/Users/Gender/{gender}<br />GET /Api/Users/Nationality/{nationality}</code></pre>

        <table class="apiTableInfo">
            <thead>
                <tr>
                    <th>Variable</th>
                    <th>Description</th>
                    <th>Format</th>
                </tr>
            </thead>
            <tbody>
                <tr>
                    <td class="variableTd">{gender}</td>
                    <td>Retrieve all users with this gender.</td>
                    <td>String, possible types: Male, Female</td>
                </tr>
                <tr>
                    <td class="variableTd">{nationality}</td>
                    <td>Retrieve all users with this nationality.</td>
                    <td>String, e.g Bulgaria</td>
                </tr>
            </tbody>
        </table>

        <h3>Request Examples</h3>
        <pre><code>GET /Api/Users<br />GET /Api/Users/Gender/Male<br />GET /Api/Users/Nationality/Bulgaria</code></pre>
    </div>
</asp:Content>
