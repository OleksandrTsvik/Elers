import { Col, Divider, Row } from 'antd';

type PanelComponents = { Picker: React.FC; Presets: React.FC };

export function customPanelRender(
  { Picker, Presets }: PanelComponents,
  height: React.CSSProperties['height'],
) {
  return (
    <Row wrap={false} justify="space-between">
      <Col flex="auto">
        <Picker />
      </Col>
      <Divider type="vertical" style={{ height: 'auto' }} />
      <Col span={12} style={{ height, overflowY: 'auto' }}>
        <Presets />
      </Col>
    </Row>
  );
}
