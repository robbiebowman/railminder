# Railminder

Using the DART realtime API, the app allows the user to select their station & direction. Using this, it checks for trains arriving soon and schedules a system notification on their device to alert them appropriately.

## User interaction & hard-coded values

The only user selectable values are the station (which defaults to Blackrock) and direction.

Hard-coded values are located in `Railminder.Services.Config.cs`. This captures the time it takes to reach the station, the default station, and how long before a train data is uninteresting.

## Libraries

- `Xam.Plugins.Notifier` for sending & scheduling notifications from shared code.

## Shortcomings

As the coding section was time windowed to 2 hours, some compromises were made.

- Before beginning I decided to keep the UI extremely minimal. If there was time left after the supporting services and view models were done, I could spend it on touching up the UI (alas no time was left over).
- According to the API documentation, Directions were not strictly limited to Northbound & Southbound, so I get that data by querying the trains list of a given station and use the values there. This means that if there are no trains returned here it won't list the directions in the UI. I felt the compromise of hard-coding Northbound and Southbound but dropping support for non-Dublin/Sligo services was not a good enough trade-off.
- Ideally the hard-coded values in `Railminder.Services.Config.cs` would customisable by the user in a settings page. Currently they are not.
- It would be nice for the UI to convey the various time spans in a better way. The user is never told the notification is going to pop specifically 10 mintues before the train arrives.
- 3 services & 1 view model is a good enough motivation for dependency injection. I decided against it because of time constraints. Furthermore, none of the services implement interfaces.

## Work not timed in the 2 hours

I did some exploratory work before starting my 2 hour coding window.
- Played with the DART realtime API
- Designed project topology (services, UX flow, end points needed)
- Practiced XML parsing in C#

## Miscellaneous/Fun

Rather than painfully copying the element names from an XML document you want to a POCO for, try pasting the element in to Visual Studio and doing a regex search and replace over it:

        <TrainCode>E109 </TrainCode>
        <TrainDate>21 Dec 2011</TrainDate>
        
Find: `<(\w+)>[^>]*>`

Replace: `[XmlElement("$1")]\npublic string $1 { get; set; }`

This will spit out:

        [XmlElement("TrainCode")]
        public string TrainCode { get; set; }

        [XmlElement("TrainDate")]
        public string TrainDate { get; set; }
        
Regex can _solve_ problems too!
