#!/bin/bash

[[ -z $SHA ]] && SHA=${1}
MSG=$(git log -1 --pretty="%B" "${SHA}")

(( LIMIT=1000 ))
(( COUNT=0 ))
for FILE in $(git ls-files --cached --others --deleted --modified --exclude-standard); do
  git add "${FILE}"
  (( COUNT=COUNT+1 ))
  if (( COUNT == LIMIT )); then
    git commit -m "PARTIAL COMMIT: ${MSG}"
    COUNT=0
  fi
done

git commit -m "PARTIAL COMMIT: ${MSG}"