module.exports = {
  root: true,
  env: { browser: true, es2020: true },
  extends: [
    'eslint:recommended',
    'plugin:@typescript-eslint/recommended-type-checked',
    'plugin:react-hooks/recommended',
    'plugin:import/recommended',
    'plugin:import/typescript',
    'plugin:prettier/recommended',
  ],
  ignorePatterns: ['dist', '.eslintrc.cjs'],
  parser: '@typescript-eslint/parser',
  parserOptions: {
    ecmaVersion: 'latest',
    sourceType: 'module',
    project: ['./tsconfig.json', './tsconfig.node.json'],
    tsconfigRootDir: __dirname,
  },
  plugins: ['react-refresh', 'prettier'],
  rules: {
    '@typescript-eslint/no-misused-promises': 'off',
    'react/react-in-jsx-scope': 'off',
    'no-console': 'warn',
    quotes: ['warn', 'single'],
    'jsx-quotes': ['warn', 'prefer-double'],
    'import/order': [
      'warn',
      {
        groups: [
          'builtin',
          'external',
          ['internal', 'parent', 'sibling', 'index'],
          'object',
          'type',
        ],
        pathGroups: [
          {
            pattern: 'antd/locale/*',
            group: 'external',
            position: 'after',
          },
          {
            pattern: 'dayjs/locale/*',
            group: 'type',
            position: 'after',
          },
          {
            patternOptions: {
              dot: true,
              nocomment: true,
              matchBase: true,
            },
            pattern: '*.{svg,png,jpg}',
            group: 'type',
            position: 'after',
          },
          {
            patternOptions: {
              dot: true,
              nocomment: true,
              matchBase: true,
            },
            pattern: '*.{css,scss}',
            group: 'type',
            position: 'after',
          },
          {
            pattern: './locales/**',
            group: 'type',
            position: 'after',
          },
          {
            pattern: './i18n',
            group: 'type',
            position: 'after',
          },
        ],
        warnOnUnassignedImports: true,
        pathGroupsExcludedImportTypes: [],
        alphabetize: {
          order: 'asc',
          caseInsensitive: true,
        },
        'newlines-between': 'always',
      },
    ],
  },
};
