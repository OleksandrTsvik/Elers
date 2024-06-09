import { Button, Form, Input, InputNumber } from 'antd';
import { useEffect } from 'react';
import { useTranslation } from 'react-i18next';

import { TestQuestionFormValues, useBaseTestQuestionRules } from './base';
import useQuestionInputRules from './use-question-input.rules';
import { ErrorForm } from '../../../common/error';
import { TextEditor } from '../../../common/typography';
import { stringToInputNumber } from '../../../utils/helpers';

export interface QuestionInputFormValues extends TestQuestionFormValues {
  answer: string;
}

interface Props {
  initialValues: QuestionInputFormValues;
  textOnSubmitButton: string;
  isLoading: boolean;
  error: unknown;
  onSubmit: (values: QuestionInputFormValues) => Promise<void> | void;
}

export default function QuestionInputForm({
  initialValues,
  textOnSubmitButton,
  isLoading,
  error,
  onSubmit,
}: Props) {
  const { t } = useTranslation();

  const [form] = Form.useForm<QuestionInputFormValues>();

  const baseRules = useBaseTestQuestionRules();
  const rules = useQuestionInputRules();

  useEffect(() => {
    form.setFieldsValue(initialValues);
  }, [form, initialValues]);

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
        name="text"
        label={t('course_material.question')}
        rules={baseRules.text}
      >
        <TextEditor editorKey="text" />
      </Form.Item>

      <Form.Item
        hasFeedback
        name="points"
        label={t('course_test.points')}
        rules={baseRules.points}
      >
        <InputNumber
          className="w-100"
          min={0.1}
          parser={(displayValue) =>
            stringToInputNumber(displayValue, true, true)
          }
        />
      </Form.Item>

      <Form.Item
        hasFeedback
        name="answer"
        label={t('course_material.answer')}
        rules={rules.answer}
      >
        <Input />
      </Form.Item>

      <Form.Item className="text-right">
        <Button type="primary" htmlType="submit" loading={isLoading}>
          {textOnSubmitButton}
        </Button>
      </Form.Item>
    </Form>
  );
}
