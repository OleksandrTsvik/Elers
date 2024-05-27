# Elers/Web

This project is a user interface for the Elers project, developed using `React` + `TypeScript` + `Vite` + `RTK` + `Antd` + `i18next` + `ckeditor5` + `react-cropper`.

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

## Setting up CKEditor 5

[CKEditor 5](https://ckeditor.com/docs/ckeditor5/latest/index.html) is a convenient and multifunctional text editor.

[Connection/update instructions](https://ckeditor.com/docs/ckeditor5/latest/installation/integrations/react/react.html#customizing-the-builds):

0. If ckeditor5 is already installed in the project, uninstall it with the command:

```sh
rm -rf ckeditor5 && npm uninstall --save @ckeditor/ckeditor5-react ckeditor5-custom-build
```

1. Go to the CKEditor 5 [online builder](https://ckeditor.com/ckeditor-5/online-builder/).
2. Step 2: Make sure the **Watchdog** plugin is not selected.
3. Step 4: Choose **Ukrainian** as the default editor language.
4. Download the generated build _(.zip)_ and unpack it in the root of the project and name the directory as `ckeditor5`.
5. Install ckeditor5 for React:

```sh
npm install --save @ckeditor/ckeditor5-react
```

6. Add the package located in the `ckeditor5` directory as a dependency of your project:

```sh
npm add file:./ckeditor5
```

### Useful information:

- [CKEditor 5 supports multiple UI languages.](https://ckeditor.com/docs/ckeditor5/latest/installation/integrations/react/react.html#localization)
- [Using css properties, you can customize the color mode of the editor.](https://ckeditor.com/docs/ckeditor5/latest/examples/framework/theme-customization.html)
- [Add the `ck-content` class to your content container to make ckeditor5 styles work.](https://ckeditor.com/docs/ckeditor5/latest/installation/advanced/content-styles.html#the-full-list-of-content-styles)
- [Configure image upload to your own api.](https://ckeditor.com/docs/ckeditor5/latest/features/images/image-upload/simple-upload-adapter.html)

## react-cropper

### Useful information:

- [Cropper.js options.](https://github.com/fengyuanchen/cropperjs?tab=readme-ov-file#options)
