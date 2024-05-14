import { Form, FormInstance, theme } from 'antd';
import { useEffect } from 'react';
import { useTranslation } from 'react-i18next';

import { ErrorForm } from '../../../common';
import { ColorPickerWithHorizontalPresets } from '../../../components';
import useColorPresets from '../../../hooks/use-color.presets';
import { parseColorPickerValue } from '../../../utils/helpers';

export interface TabColorFormValues {
  color?: string;
}

interface Props {
  initialValues: TabColorFormValues;
  tabName: string;
  error: unknown;
  onSubmit: (values: TabColorFormValues) => Promise<void> | void;
  onFormInstanceReady?: (instance: FormInstance<TabColorFormValues>) => void;
}

export default function TabColorForm({
  initialValues,
  tabName,
  error,
  onSubmit,
  onFormInstanceReady,
}: Props) {
  const { t } = useTranslation();

  const {
    token: { colorText },
  } = theme.useToken();

  const presets = useColorPresets();
  const [form] = Form.useForm<TabColorFormValues>();

  useEffect(() => {
    onFormInstanceReady && onFormInstanceReady(form);
  }, [form, onFormInstanceReady]);

  return (
    <Form
      form={form}
      layout="vertical"
      initialValues={initialValues}
      onFinish={onSubmit}
    >
      <ErrorForm error={error} form={form} />

      <Form.Item
        hasFeedback
        name="color"
        label={t('course.tab_color')}
        getValueFromEvent={parseColorPickerValue}
      >
        <ColorPickerWithHorizontalPresets
          allowClear
          presets={presets}
          showText={(color) => (
            <span
              style={{ color: color.cleared ? colorText : color.toHexString() }}
            >
              {tabName}
            </span>
          )}
        />
      </Form.Item>
    </Form>
  );
}
