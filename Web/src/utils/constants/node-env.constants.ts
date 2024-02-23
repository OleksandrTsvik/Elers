export const NODE_ENV = import.meta.env.MODE;
export const IS_DEVELOPMENT = import.meta.env.DEV;
export const IS_PRODUCTION = import.meta.env.PROD;

export const REACT_APP_API_URL = import.meta.env
  .VITE_REACT_APP_API_URL as string;
