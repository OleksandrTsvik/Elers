# Elers/Web

This project is a user interface for the Elers project, developed using `React` + `TypeScript` + `Vite` + `RTK` + `Antd` + `i18next`.

## Running Locally

To run the project locally, first, you need to install dependencies:

```sh
cd Web
npm install
```

Then, start the project:

```sh
npm run dev
```

Then open [http://localhost:3000/](http://localhost:3000/) to see Elers web app.

## Environment Variables Configuration

The `.env` file contains the environment variables required for the project to work. For example, `VITE_REACT_APP_API_URL` indicates the address of the server API:

```ini
VITE_REACT_APP_API_URL=http://localhost:5000/api
```

## Setting up i18n Ally extension

For convenient working with the [`react-i18next`](https://react.i18next.com/) framework for app localization, you can use the [`i18n Ally`](https://github.com/lokalise/i18n-ally) extension for `VS Code`.

Its settings for VS Code can be placed in the root of the entire project at the path `.vscode/settings.json`:

```json
{
  "i18n-ally.enabledFrameworks": ["react-i18next"],
  "i18n-ally.localesPaths": "Web/public/locales",
  "i18n-ally.pathMatcher": "{locale}/{namespaces}.json",
  "i18n-ally.keystyle": "nested",
  "i18n-ally.sourceLanguage": "uk",
  "i18n-ally.displayLanguage": "uk",
  "i18n-ally.sortKeys": true
}
```
