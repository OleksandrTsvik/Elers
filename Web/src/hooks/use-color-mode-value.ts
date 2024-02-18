import useColorMode from './use-color-mode';

// https://github.com/chakra-ui/chakra-ui/blob/main/packages/components/src/color-mode/color-mode-context.ts
export function useColorModeValue<TLight = unknown, TDark = unknown>(
  light: TLight,
  dark: TDark,
) {
  const { colorMode } = useColorMode();

  return colorMode === 'light' ? light : dark;
}
