import { red } from '@ant-design/colors';
import { MinusCircleOutlined, PlusOutlined } from '@ant-design/icons';
import { Button, Flex, Form, Input, InputNumber } from 'antd';
import { useEffect } from 'react';
import { useTranslation } from 'react-i18next';

import { TestQuestionFormValues, useBaseTestQuestionRules } from './base';
import useQuestionMatchingRules from './use-question-matching.rules';
import { ErrorForm } from '../../../common/error';
import { TextEditor } from '../../../common/typography';
import { TestQuestionMatchOption } from '../../../models/test-question.interface';
import { stringToInputNumber } from '../../../utils/helpers';

export interface QuestionMatchingFormValues extends TestQuestionFormValues {
  options: TestQuestionMatchOption[];
}

interface Props {
  initialValues: QuestionMatchingFormValues;
  textOnSubmitButton: string;
  isLoading: boolean;
  error: unknown;
  onSubmit: (values: QuestionMatchingFormValues) => Promise<void> | void;
}

export default function QuestionMatchingForm({
  initialValues,
  textOnSubmitButton,
  isLoading,
  error,
  onSubmit,
}: Props) {
  const { t } = useTranslation();

  const [form] = Form.useForm<QuestionMatchingFormValues>();

  const baseRules = useBaseTestQuestionRules();
  const rules = useQuestionMatchingRules();

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

      <Form.List name="options" rules={rules.options}>
        {(fields, { add, remove }, { errors }) => (
          <>
            <div className="pb-label-field">
              {t('course_test.answer_options')}:
            </div>

            {fields.map(({ key, name }) => (
              <Flex
                key={key}
                className="text-center mb-field"
                align="middle"
                justify="center"
              >
                <Form.Item className="m-0 w-100 pe-3" name={[name, 'question']}>
                  <Input placeholder={t('course_test.question')} />
                </Form.Item>
                <Form.Item className="m-0 w-100" name={[name, 'answer']}>
                  <Input placeholder={t('course_test.answer')} />
                </Form.Item>
                <MinusCircleOutlined
                  className="ps-3"
                  style={{ color: red.primary }}
                  onClick={() => remove(name)}
                />
              </Flex>
            ))}

            <Form.Item>
              <Button
                block
                type="dashed"
                icon={<PlusOutlined />}
                onClick={() => add()}
              >
                {t('course_test.add_answer_option')}
              </Button>
              <Form.ErrorList errors={errors} />
            </Form.Item>
          </>
        )}
      </Form.List>

      <Form.Item className="text-right">
        <Button type="primary" htmlType="submit" loading={isLoading}>
          {textOnSubmitButton}
        </Button>
      </Form.Item>
    </Form>
  );
}
