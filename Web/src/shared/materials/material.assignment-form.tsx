import { Button, DatePicker, Form, Input, InputNumber, Radio } from 'antd';
import { useTranslation } from 'react-i18next';

import useMaterialAssignmentRules from './use-material-assignment.rules';
import { ErrorForm } from '../../common/error';
import { COURSE_MATERIAL_RULES } from '../../common/rules';
import { TextEditor } from '../../common/typography';
import { DATE_FORMAT } from '../../utils/constants/app.constants';
import { stringToInputNumber } from '../../utils/helpers';

export interface MaterialAssignmentFormValues {
  title: string;
  description: string;
  deadline?: Date;
  maxFiles: number;
  maxGrade: number;
}

const maxFilesOptions = Array(COURSE_MATERIAL_RULES.assignment.maxFiles.max + 1)
  .fill(null)
  .map((_, i) => i);

interface Props {
  initialValues: MaterialAssignmentFormValues;
  textOnSubmitButton: string;
  isLoading: boolean;
  error: unknown;
  onSubmit: (values: MaterialAssignmentFormValues) => Promise<void> | void;
}

export function MaterialAssignmentForm({
  initialValues,
  textOnSubmitButton,
  isLoading,
  error,
  onSubmit,
}: Props) {
  const { t } = useTranslation();

  const [form] = Form.useForm<MaterialAssignmentFormValues>();
  const rules = useMaterialAssignmentRules();

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
        name="title"
        label={t('course_material.title')}
        rules={rules.title}
      >
        <Input
          showCount
          maxLength={COURSE_MATERIAL_RULES.assignment.title.max}
        />
      </Form.Item>

      <Form.Item
        hasFeedback
        name="description"
        label={t('course_material.assignment_description')}
        rules={rules.description}
      >
        <TextEditor
          editorKey="description"
          text={form.getFieldValue('description') as string}
          onChange={(text) => form.setFieldValue('description', text)}
        />
      </Form.Item>

      <Form.Item
        hasFeedback
        name="deadline"
        label={t('course_material.deadline')}
        rules={rules.deadline}
      >
        <DatePicker className="w-100" format={DATE_FORMAT} />
      </Form.Item>

      <Form.Item
        hasFeedback
        name="maxFiles"
        label={t('course_material.assignment_max_files')}
        rules={rules.maxFiles}
      >
        <Radio.Group
          optionType="button"
          buttonStyle="solid"
          options={maxFilesOptions}
        />
      </Form.Item>

      <Form.Item
        hasFeedback
        name="maxGrade"
        label={`${t('course_material.max_grade')} (${COURSE_MATERIAL_RULES.assignment.maxGrade.min}-${COURSE_MATERIAL_RULES.assignment.maxGrade.max})`}
        rules={rules.maxGrade}
      >
        <InputNumber
          className="w-100"
          min={COURSE_MATERIAL_RULES.assignment.maxGrade.min}
          max={COURSE_MATERIAL_RULES.assignment.maxGrade.max}
          parser={(displayValue) => stringToInputNumber(displayValue)}
        />
      </Form.Item>

      <Form.Item className="text-right">
        <Button type="primary" htmlType="submit" loading={isLoading}>
          {textOnSubmitButton}
        </Button>
      </Form.Item>
    </Form>
  );
}
