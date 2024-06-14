import { DatePicker, Form, FormInstance, Input, InputNumber } from 'antd';
import dayjs from 'dayjs';
import { useEffect } from 'react';
import { useTranslation } from 'react-i18next';

import useGradesColumnRules from './use-grades-column.rules';
import { ErrorForm } from '../../../common/error';
import { GRADE_RULES } from '../../../common/rules';
import { DATE_FORMAT } from '../../../utils/constants/app.constants';
import { stringToInputNumber } from '../../../utils/helpers';

export interface GradesColumnFormValues {
  title: string;
  date: Date;
  maxGrade: number;
}

interface Props {
  initialValues: GradesColumnFormValues;
  error: unknown;
  onSubmit: (values: GradesColumnFormValues) => Promise<void> | void;
  onFormInstanceReady: (instance: FormInstance<GradesColumnFormValues>) => void;
}

export default function GradesColumnForm({
  initialValues,
  error,
  onSubmit,
  onFormInstanceReady,
}: Props) {
  const { t } = useTranslation();

  const [form] = Form.useForm<GradesColumnFormValues>();
  const rules = useGradesColumnRules();

  useEffect(() => {
    form.setFieldsValue({
      ...initialValues,
      date: dayjs(initialValues.date),
    });
  }, [form, initialValues]);

  useEffect(() => {
    onFormInstanceReady(form);
  }, [form, onFormInstanceReady]);

  return (
    <Form form={form} layout="vertical" onFinish={onSubmit}>
      <ErrorForm error={error} form={form} />

      <Form.Item
        name="title"
        label={t('course_grades_page.column_title')}
        rules={rules.title}
      >
        <Input showCount maxLength={GRADE_RULES.title.max} />
      </Form.Item>

      <Form.Item
        name="date"
        label={t('course_grades_page.column_date')}
        rules={rules.date}
      >
        <DatePicker className="w-100" format={DATE_FORMAT} />
      </Form.Item>

      <Form.Item
        name="maxGrade"
        label={`${t('course_grades_page.column_max_grade')} (${GRADE_RULES.maxGrade.min}-${GRADE_RULES.maxGrade.max})`}
        rules={rules.maxGrade}
      >
        <InputNumber
          className="w-100"
          min={GRADE_RULES.maxGrade.min}
          max={GRADE_RULES.maxGrade.max}
          parser={(displayValue) => stringToInputNumber(displayValue)}
        />
      </Form.Item>
    </Form>
  );
}
