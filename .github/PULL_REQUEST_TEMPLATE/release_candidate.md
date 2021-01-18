---
name: Release
about: Only for use when merging a sprint's deliverable into main. Please don't use this template for anything else.
title: 'Release the sprint x candidate '
assignees: ''

---

<!--
Release routine checklist:
 * Capture the `develop` branch as `Candidate-Release/x`. This is the candidate for release.
 * Open PR, filling appropriate details.
 * Request review from Gal, and only Gal. He will act as a proxy for bringing fauna's feedback to the repository.
 * Ensure a build check has run, and passed - and that Mac and Win copies are available for review.
 * Ensure ALL `x`'s have been replaced with the sprint number.
 * Add to sprint milestone
 * (Shinkson will) record code stats from the candidate on the google drive.
-->

Once accepted, this will be version `pre-release 0.x.0`
<!-- 
Use semantic versioning, where:
 * The center digit represents the release number, i.e the second release = 0.2.0
 and
 * the last digit represents the count of hot fixes appended to the release after the candidate was captured.
-->

> Please append any notes, feedback, or other data relavent to this release to this PR for best historical recording. 

> _NB_ : This is a publicly available record of our release.

This PR finalizes the changes implemented by the sprint x, as listed in the [card](<!-- link to project board card for this sprint -->). 

This PR will remain open until Production / Client have provided thier acceptance, or permenant disproval (disproval for this release, which will not be hot-fixed).

# Sprint goal
<!-- See sprint goal in the card -->

# Changelist
<!-- A brief overview of the changes brought by the sprint -->
 * Implemented a new feature
 * Fixed major bug 

# Card list
<!-- Copy the items in the card, implementing 'closes' or 'closed' on the various issues. -->

## User stories
 - closed #9999 : Fancy new system section story

## Includes epic
 - #9999 : System epic

## Fixes
 - fixes #9999 : Saving bug

## Merges
 - closed #9999 : Feature PR
