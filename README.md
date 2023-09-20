# Top Hacker News API
## How to run
Under the TopHackerNewApi directory, run the following command:

```dotnet run```

API will be ready on port 5053

You can then run a curl command like this:

```curl -i 'http://localhost:5033/TopStories?topN=5'```

Alternatively, you can run the application via Visual Studio, giving you a swagger UI to test the API.

## Potential enhancements and considerations
- Do not mix up integration and unit tests. HackerRankClientTest is an integration test which hits the actual hacker rank API, which is not ideal. 
- Dockerise the service and write some acceptance tests against the docker container.
- Consider unexpected responses from HackerRank, what to do if they are down, or for some reason we're throttled. Do we want to return an error message to our clients, or do we want to hide the issue from them
- Cache timeout? What is an acceptable time for business? How stale our responses can be?
- Ideally, create another backend service that pulls HackerRank API and populates a database. This way, we can have a more reliable and faster API.
  - Currently, the first client who hits our API will be penalised by the time it takes to pull the data from HackerRank API. 
