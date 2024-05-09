import { ColorPicker, GetProps } from 'antd';

import { customPanelRender } from './panel-render';

type ColorPickerProps = GetProps<typeof ColorPicker>;

type Props = Omit<ColorPickerProps, 'styles' | 'panelRender'> &
  Required<Pick<ColorPickerProps, 'presets'>> & {
    width?: React.CSSProperties['width'];
    height?: React.CSSProperties['height'];
  };

export default function ColorPickerWithHorizontalPresets({
  width = 520,
  height = 262,
  ...props
}: Props) {
  return (
    <ColorPicker
      styles={{ popupOverlayInner: { width } }}
      panelRender={(_, extra) => customPanelRender(extra.components, height)}
      {...props}
    />
  );
}
