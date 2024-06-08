import { Button, DatePicker, Form, Input, InputNumber } from 'antd';
import dayjs from 'dayjs';
import { useTranslation } from 'react-i18next';

import useMaterialTestRules from './use-material-test.rules';
import { ErrorForm } from '../../common/error';
import { COURSE_MATERIAL_RULES } from '../../common/rules';
import { TextEditor } from '../../common/typography';
import { DATE_FORMAT } from '../../utils/constants/app.constants';
import { stringToInputNumber } from '../../utils/helpers';

export interface MaterialTestFormValues {
  title: string;
  description?: string;
  numberAttempts: number;
  timeLimitInMinutes?: number;
  deadline?: Date;
}

interface Props {
  initialValues: MaterialTestFormValues;
  textOnSubmitButton: string;
  isLoading: boolean;
  error: unknown;
  onSubmit: (values: MaterialTestFormValues) => Promise<void> | void;
}

export function MaterialTestForm({
  initialValues,
  textOnSubmitButton,
  isLoading,
  error,
  onSubmit,
}: Props) {
  const { t } = useTranslation();

  const [form] = Form.useForm<MaterialTestFormValues>();
  const rules = useMaterialTestRules();

  return (
    <Form
      form={form}
      layout="vertical"
      initialValues={{
        ...initialValues,
        deadline: initialValues.deadline
          ? dayjs(initialValues.deadline)
          : undefined,
      }}
      onFinish={onSubmit}
    >
      <ErrorForm error={error} form={form} />

      <Form.Item
        hasFeedback
        name="title"
        label={t('course_material.title')}
        rules={rules.title}
      >
        <Input showCount maxLength={COURSE_MATERIAL_RULES.test.title.max} />
      </Form.Item>

      <Form.Item
        hasFeedback
        name="description"
        label={t('course_test.description')}
        rules={rules.description}
      >
        <TextEditor editorKey="description" />
      </Form.Item>

      <Form.Item
        hasFeedback
        name="numberAttempts"
        label={t('course_test.number_attempts')}
        rules={rules.numberAttempts}
      >
        <InputNumber
          className="w-100"
          min={COURSE_MATERIAL_RULES.test.numberAttempts.min}
          max={COURSE_MATERIAL_RULES.test.numberAttempts.max}
          parser={stringToInputNumber}
        />
      </Form.Item>

      <Form.Item
        hasFeedback
        name="timeLimitInMinutes"
        label={t('course_test.time_limit')}
        rules={rules.timeLimitInMinutes}
      >
        <InputNumber
          className="w-100"
          min={COURSE_MATERIAL_RULES.test.timeLimitInMinutes.min}
          max={COURSE_MATERIAL_RULES.test.timeLimitInMinutes.max}
          parser={stringToInputNumber}
        />
      </Form.Item>

      <Form.Item
        hasFeedback
        name="deadline"
        label={t('course_test.deadline')}
        rules={rules.deadline}
      >
        <DatePicker className="w-100" format={DATE_FORMAT} />
      </Form.Item>

      <Form.Item className="text-right">
        <Button type="primary" htmlType="submit" loading={isLoading}>
          {textOnSubmitButton}
        </Button>
      </Form.Item>
    </Form>
  );
}
