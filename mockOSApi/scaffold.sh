#!/bin/bash

dotnet aspnet-codegenerator controller -name ThreadController -m mockOSApi.Models.Thread  --relativeFolderPath Controllers --useDefaultLayout --referenceScriptLibraries --dataContext mockOSApi.Data.ApplicationDbContext

