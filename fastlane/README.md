fastlane documentation
----

# Installation

Make sure you have the latest version of the Xcode command line tools installed:

```sh
xcode-select --install
```

For _fastlane_ installation instructions, see [Installing _fastlane_](https://docs.fastlane.tools/#installing-fastlane)

# Available Actions

## iOS

### ios init_ci

```sh
[bundle exec] fastlane ios init_ci
```



### ios sync_certificates

```sh
[bundle exec] fastlane ios sync_certificates
```

Sync codesigning certificates

### ios release

```sh
[bundle exec] fastlane ios release
```

Deliver a new Release build to the App Store

### ios beta

```sh
[bundle exec] fastlane ios beta
```

Deliver a new Beta build to Apple TestFlight

### ios build

```sh
[bundle exec] fastlane ios build
```

Create .ipa

----

This README.md is auto-generated and will be re-generated every time [_fastlane_](https://fastlane.tools) is run.

More information about _fastlane_ can be found on [fastlane.tools](https://fastlane.tools).

The documentation of _fastlane_ can be found on [docs.fastlane.tools](https://docs.fastlane.tools).
