import { Form, FormInstance, Input } from 'antd';
import { useEffect } from 'react';
import { useTranslation } from 'react-i18next';

import { ErrorForm } from '../../../common';
import { COURSE_RULES } from '../../../common/rules';
import useTabRules from '../tabs/use-tab.rules';

export interface TabNameFormValues {
  tabName: string;
}

interface Props {
  initialValues: TabNameFormValues;
  error: unknown;
  onSubmit: (values: TabNameFormValues) => Promise<void> | void;
  onFormInstanceReady?: (instance: FormInstance<TabNameFormValues>) => void;
}

export default function TabNameForm({
  initialValues,
  error,
  onSubmit,
  onFormInstanceReady,
}: Props) {
  const { t } = useTranslation();

  const [form] = Form.useForm<TabNameFormValues>();
  const rules = useTabRules();

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
      <ErrorForm error={error} />

      <Form.Item
        hasFeedback
        name="tabName"
        label={t('course.section_name')}
        rules={rules.tabName}
      >
        <Input showCount maxLength={COURSE_RULES.tabName.max} />
      </Form.Item>
    </Form>
  );
}
