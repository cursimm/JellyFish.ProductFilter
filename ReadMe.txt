Project has been developed in visual studio 2019 and targets .net core framework 3.1.

The UI and API can both be run simultaneously from visual studio.

Just ensure that you configure both JellyFish.ProductFilter.Api and JellyFish.ProductFilter.UI as startup projects.

Initial build of JellyFish.ProductFilter.UI will take a while as the node_modules folder for the react ui needs to be restored.

Given a larger project - services, data etc would be refactored in to their wn projects, not necessary on this scale.
