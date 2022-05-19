#!/bin/bash

dotnet clean ../src/Spacetime -c:Release
dotnet build ../src/Spacetime/ -f:net6.0-maccatalyst -c:Release
