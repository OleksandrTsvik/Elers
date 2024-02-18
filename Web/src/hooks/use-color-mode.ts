import { useAppDispatch, useAppSelector } from './store';
import {
  ColorMode,
  selectColorMode,
  toogleColorMode as toogleColor,
  setColorMode as setColor,
} from '../store/color-mode.slice';

export default function useColorMode() {
  const appDispatch = useAppDispatch();

  const colorMode = useAppSelector(selectColorMode);

  const setColorMode = (mode: ColorMode) => {
    appDispatch(setColor(mode));
  };

  const toggleColorMode = () => {
    appDispatch(toogleColor());
  };

  return { colorMode, setColorMode, toggleColorMode };
}
